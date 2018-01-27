using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerSpawner : MonoBehaviour {

    [Range(1,30)]
    public int spawningInterval = 10; // seconds

    [Range(1, 10)]
    public int numberOfBeerUnitsPerRound = 10;

    public GameObject beerUnitPrefab;

    private float waitingForNewRoundTimer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (waitingForNewRoundTimer >= spawningInterval)
        {
            waitingForNewRoundTimer = 0.0f;
            DispatchAnotherRound();
        }
        else
        {
            waitingForNewRoundTimer += Time.deltaTime;
        }
	}

    void DispatchAnotherRound()
    {
        // position beer units initialy
        for (int i = 0; i < numberOfBeerUnitsPerRound; i++)
        {
            GameObject beerUnitInstance = Instantiate(beerUnitPrefab) as GameObject;
            beerUnitInstance.transform.position = new Vector2(0.0f, i);
        }
    }
}
