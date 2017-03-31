using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfCurve { triangular, trapezoid};
public enum TypeOfVariable { input, output};

[System.Serializable]
public class MembershipFunctionData
{
    public string membershipFunctionName = "";
    public TypeOfCurve curveShape = TypeOfCurve.triangular;
    public Vector3 triangularParameters = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector4 trapezoidParameters = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
}

[System.Serializable]
public class FuzzyVariableData
{
    public string variableName = "";
    public TypeOfVariable varType = TypeOfVariable.input;
    public List<MembershipFunctionData> membershipFunctions = new List<MembershipFunctionData>();
}

[System.Serializable]
public class RuleVariableData
{
    public string name = "";
    public string condition = "";
}

[System.Serializable]
public class RuleData
{
    public RuleVariableData input1;
    public RuleVariableData input2;
    public RuleVariableData output;
}

