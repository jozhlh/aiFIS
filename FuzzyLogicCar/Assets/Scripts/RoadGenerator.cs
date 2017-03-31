using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject roadTilePrefab = null;

    [SerializeField]
    private GameObject car = null;

    [SerializeField]
    private float spacing = 20.0f;

    [SerializeField]
    private int lengthOfRoad = 20;

    [SerializeField]
    private float cullingDistance = 20.0f;

    private List<GameObject> activeTiles;

	// Use this for initialization
	void Start ()
    {
        activeTiles = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        AddTile();
        CleanUp();
	}

    void AddTile()
    {
        while (activeTiles.Count < lengthOfRoad)
        {
            if (activeTiles.Count > 0)
            {
                Vector3 creationPosition = activeTiles[activeTiles.Count - 1].transform.position;
                creationPosition.z += spacing;
                activeTiles.Add(Instantiate(roadTilePrefab, creationPosition, transform.rotation));
            }
            else
            {
                activeTiles.Add(Instantiate(roadTilePrefab));
            }
        }
    }

    void CleanUp()
    {
        float tilePos = activeTiles[0].transform.position.z + cullingDistance;
        if (tilePos < car.transform.position.z)
        {
            GameObject tile = activeTiles[0];
            activeTiles.RemoveAt(0);
            Destroy(tile);
        }
    }
}
