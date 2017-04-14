using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class AiInterface : MonoBehaviour {

    [SerializeField]
    private GameObject car = null;

    [SerializeField]
    private GameObject racingLine = null;

    [SerializeField]
    private MandaniController fuzzySystem = null;

    [SerializeField]
    private float roadWidth = 10.0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        fuzzySystem.inputs[0] = CalculateDisplacement();
        fuzzySystem.inputs[1] = car.GetComponent<CarAiInputController>().GetRateOfDisplacement();

        car.GetComponent<CarAiInputController>().SetSteering((float)fuzzySystem.EvaluateSystem());
    }

    float CalculateDisplacement()
    {
        float displacement = 0.0f;

        displacement = car.transform.position.x - racingLine.transform.position.x;

        displacement = displacement / roadWidth;

        displacement = fClamp(displacement, -1.0f, 1.0f);

        return displacement;
    }

    float fClamp(float x, float min, float max)
    {
        float val = x;

        if (x < min)
        {
            val = min;
        }
        else if (x > max)
        {
            val = max;
        }

        return val;
    }
}
