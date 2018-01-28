using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerSpawner : MonoBehaviour {

    [Range(1,30)]
    public int spawningInterval = 10; // seconds

    [Range(1, 10)]
    public int numberOfBeerUnitsPerRound = 10;

    [Range(0, 2)]
    public int beerColorIndex = 0;

    public Sprite[] beerColorSprites;

    public GameObject beerUnitPrefab;

    private float waitingForNewRoundTimer = 0.0f;
    private System.Random r = new System.Random(System.DateTime.Now.Millisecond);

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
        // position beer units initialy and give them sprites
        beerColorIndex = r.Next(3);
        for (int i = 0; i < numberOfBeerUnitsPerRound; i++)
        {
            GameObject beerUnitInstance = Instantiate(beerUnitPrefab) as GameObject;
            beerUnitInstance.transform.position = new Vector2(0.0f, i);
            beerUnitInstance.GetComponent<SpriteRenderer>().sprite = beerColorSprites[beerColorIndex];
            beerUnitInstance.tag = TagResolver.GetNameByIndex(beerColorIndex);
        }
    }
}
