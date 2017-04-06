using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Fuzzy.Library;

public class MandaniController : MonoBehaviour
{

    private MamdaniFuzzySystem fsCar = null;

    public List<float> inputs = new List<float>();

    [SerializeField]
    private List<FuzzyVariableData> variables = new List<FuzzyVariableData>();

    [SerializeField]
    private List<RuleData> fsRules = new List<RuleData>();

    private float lowerRange = -1.0f;
    private float upperRange = 1.0f;

    private List<FuzzyVariable> fsInputVariables = new List<FuzzyVariable>();
    private FuzzyVariable fsOutputVariable;

    void Start()
    {
        CreateSystem();
    }

    void CreateSystem()
    {
        //
        // Create empty fuzzy system
        //
        fsCar = new MamdaniFuzzySystem();

        //
        // Create input variables and rules for the system
        //
        CreateVariables();
        CreateRules();
    }

    private void CreateVariables()
    {
        // for each input or output variable entered in the inspector
        foreach (FuzzyVariableData fsVar in variables)
        {
            // create a new variable with the correct name
            FuzzyVariable fv = new FuzzyVariable(fsVar.variableName, lowerRange, upperRange);

            // for each membership function (term) of the variable
            foreach (MembershipFunctionData mf in fsVar.membershipFunctions)
            {
                // add the membership function based on the shape selected
                switch (mf.curveShape)
                {
                    case TypeOfCurve.triangular:
                        fv.Terms.Add(new FuzzyTerm(mf.membershipFunctionName,
                            new TriangularMembershipFunction(mf.triangularParameters.x, mf.triangularParameters.y, mf.triangularParameters.z)));
                        break;
                    case TypeOfCurve.trapezoid:
                        fv.Terms.Add(new FuzzyTerm(mf.membershipFunctionName,
                            new TrapezoidMembershipFunction(mf.trapezoidParameters.x, mf.trapezoidParameters.y, mf.trapezoidParameters.z, mf.trapezoidParameters.w)));
                        break;
                    case TypeOfCurve.bell:
                        //fv.Terms.Add(new FuzzyTerm(mf.membershipFunctionName,
                            //new TriangularMembershipFunction(mf.triangularParameters.x, mf.triangularParameters.y, mf.triangularParameters.z).ToNormalMF()));
                        fv.Terms.Add(new FuzzyTerm(mf.membershipFunctionName, new NormalMembershipFunction(mf.bellParameters.x, mf.bellParameters.y)));
                      //  fv.Terms.Add(new FuzzyTerm(mf.membershipFunctionName,
                         //   new ))
                        break;
                }

            }

            // add the variable to the fuzzy syatem and add a copy of it to the local list for convenience
            if (fsVar.varType == TypeOfVariable.input)
            {
                fsCar.Input.Add(fv);
                fsInputVariables.Add(fv);
                inputs.Add(0.0f);
            }
            else
            {
                fsCar.Output.Add(fv);
                fsOutputVariable = fv;
            }
        }
    }

    private void CreateRules()
    {
        foreach (RuleData rule in fsRules)
        {
            AddFuzzyRule(
                fsCar,
                fsCar.InputByName(rule.input1.name), 
                fsCar.InputByName(rule.input2.name), 
                fsCar.OutputByName(rule.output.name),
                rule.input1.condition, 
                rule.input2.condition, 
                rule.output.condition);
        }
    }

    private void AddFuzzyRule(
           MamdaniFuzzySystem fs,
           FuzzyVariable fv1,
           FuzzyVariable fv2,
           FuzzyVariable fv,
           string value1,
           string value2,
           string result)
    {
        MamdaniFuzzyRule rule = fs.EmptyRule();
        rule.Condition.Op = OperatorType.And;
        rule.Condition.ConditionsList.Add(rule.CreateCondition(fv1, fv1.GetTermByName(value1)));
        rule.Condition.ConditionsList.Add(rule.CreateCondition(fv2, fv2.GetTermByName(value2)));
        rule.Conclusion.Var = fv;
        rule.Conclusion.Term = fv.GetTermByName(result);
        fs.Rules.Add(rule);
    }

    public double EvaluateSystem()
    {
        double returnVal = 0.0f;

        //
        // Associate input values with input variables
        //
        Dictionary<FuzzyVariable, double> inputValues = new Dictionary<FuzzyVariable, double>();

        int iterator = 0;
        foreach (FuzzyVariable fv in fsInputVariables)
        {
            inputValues.Add(fv, inputs[iterator]);
            iterator++;
        }

        //
        // Calculate result: one output value for each output variable
        //
        Dictionary<FuzzyVariable, double> result = fsCar.Calculate(inputValues);

        //
        // Get output value
        //
        returnVal = result[fsOutputVariable];

        return returnVal;
    }
}
