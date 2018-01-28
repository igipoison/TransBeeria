using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerSpawner : MonoBehaviour {

    [Range(1,30)]
    public int MAX_SPAWNING_INTERVAL = 10; // seconds

    [Range(1, 30)]
    public int MAX_SPAWNING_START_TIME = 5; // seconds;

    [Range(1, 10)]
    public int MAX_BEER_UNITS_PER_ROUND = 10;

    [Range(0, 2)]
    public int beerColorIndex = 0;

    public Sprite[] beerColorSprites;
    public GameObject beerUnitPrefab;

    private float waitingForNewRoundTimer = 0.0f;
    private int numberOfBeerUnitsPerRound;

    // Use this for initialization
    void Start ()
    {
        waitingForNewRoundTimer = MAX_SPAWNING_START_TIME; //RandomUtils.GetRandomNumber(0, MAX_SPAWNING_START_TIME);
        numberOfBeerUnitsPerRound = MAX_BEER_UNITS_PER_ROUND; // RandomUtils.GetRandomNumber(1, MAX_BEER_UNITS_PER_ROUND);
    }
	
	// Update is called once per frame
	void Update () {
		
        if (waitingForNewRoundTimer >= MAX_SPAWNING_INTERVAL)
        {
            waitingForNewRoundTimer = 0.0f;

            // decide what is next beer
            List<HouseHandler> allHouseHandlers = LevelLoader.getInstance().GetAllHouseHandlers();
            List<BeerTag> desiredBeerTags = new List<BeerTag>();

            string debugString = "BEERS: ";
            foreach (HouseHandler houseHandler in allHouseHandlers)
            {
                if (houseHandler.getBeerTag() != BeerTag.UKNOWN)
                {
                    desiredBeerTags.Add(houseHandler.getBeerTag());
                    debugString += TagResolver.GetNameByIndex(TagResolver.GetIndexByTag(houseHandler.getBeerTag())) + " ";
                }
            }
            Debug.Log(debugString);

            if (desiredBeerTags.Count > 0)
            {
                int randomDesiredIndex = RandomUtils.GetRandomNumber(0, desiredBeerTags.Count);
                beerColorIndex = TagResolver.GetIndexByTag(desiredBeerTags[randomDesiredIndex]);
                DispatchAnotherRound();
            }
        }
        else
        {
            waitingForNewRoundTimer += Time.deltaTime;
            Game.UpdateTimeUntilNextBeer((int)(MAX_SPAWNING_INTERVAL - waitingForNewRoundTimer));
        }
	}

    void DispatchAnotherRound()
    {
        // position beer units initialy and give them sprites
        for (int i = 0; i < numberOfBeerUnitsPerRound; i++)
        {
            GameObject beerUnitInstance = Instantiate(beerUnitPrefab) as GameObject;
            beerUnitInstance.transform.position = new Vector2(0.0f, i);
            beerUnitInstance.GetComponent<SpriteRenderer>().sprite = beerColorSprites[beerColorIndex];
            beerUnitInstance.tag = TagResolver.GetNameByIndex(beerColorIndex);
        }
        Game.UpdateBeersDispatched(numberOfBeerUnitsPerRound);
    }
}
