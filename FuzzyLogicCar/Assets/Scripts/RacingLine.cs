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
	void Start ()
	{
		Input.simulateMouseWithTouches = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetMouseButton(0))
		{
			// Construct a ray from the current touch coordinates
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			// Create a particle if hit
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				linePosition = hit.point.x;
			}
		}
		gameObject.transform.position = new Vector3(linePosition, 0.0f, car.transform.position.z);
	}


}
