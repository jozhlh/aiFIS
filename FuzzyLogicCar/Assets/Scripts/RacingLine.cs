using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingLine : MonoBehaviour {

	[SerializeField]
	private GameObject car = null;

    [SerializeField]
    [Range(-10.0f, 10.0f)]
    private float linePosition = 0.0f;

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position = new Vector3(linePosition, 0.0f, car.transform.position.z);
	}


}
