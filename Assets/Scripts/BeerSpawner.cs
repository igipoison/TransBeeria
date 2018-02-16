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
    private int numberOfBeerUnitsLeftInTheRound;

    // Use this for initialization
    void Start ()
    {
        waitingForNewRoundTimer = MAX_SPAWNING_START_TIME; //RandomUtils.GetRandomNumber(0, MAX_SPAWNING_START_TIME);
        numberOfBeerUnitsPerRound = MAX_BEER_UNITS_PER_ROUND; // RandomUtils.GetRandomNumber(1, MAX_BEER_UNITS_PER_ROUND);
    }
	
	// Update is called once per frame
	void Update () {

        if (!GameManager.IsGameRunning())
            return;
		
        if (waitingForNewRoundTimer >= MAX_SPAWNING_INTERVAL)
        {
            waitingForNewRoundTimer = 0.0f;

            // decide what is the next beer
            List<HouseHandler> allHouseHandlers = LevelLoader.getInstance().GetAllHouseHandlers();
            
            Dictionary<BeerTag, int> beerDemands = new Dictionary<BeerTag, int>();
            beerDemands[BeerTag.STOUT] = 0;
            beerDemands[BeerTag.RED] = 0;
            beerDemands[BeerTag.PILSNER] = 0;

            foreach (HouseHandler houseHandler in allHouseHandlers)
            {
                if (houseHandler.getBeerTag() != BeerTag.UKNOWN)
                {
                    beerDemands[houseHandler.getBeerTag()]++;
                }
            }

            BeerTag bestBeerToDispatch = BeerTag.UKNOWN;
            int bestHouses = 0;

            foreach(var pair in beerDemands)
            {
                if (beerDemands[pair.Key] > bestHouses)
                {
                    bestHouses = pair.Value;
                    bestBeerToDispatch = pair.Key;
                }
            }

            if (bestBeerToDispatch != BeerTag.UKNOWN)
            { 
                beerColorIndex = (int)bestBeerToDispatch;
                DispatchAnotherRound();
            }
        }
        else
        {
            waitingForNewRoundTimer += Time.deltaTime;
            GameManager.UpdateTimeUntilNextBeer((int)(MAX_SPAWNING_INTERVAL - waitingForNewRoundTimer));
        }
	}

    void DispatchAnotherRound()
    {
        numberOfBeerUnitsLeftInTheRound = numberOfBeerUnitsPerRound;

        // position beer units initialy and give them sprites
        for (int i = 0; i < numberOfBeerUnitsPerRound; i++)
        {
            GameObject beerUnitInstance = Instantiate(beerUnitPrefab) as GameObject;
            beerUnitInstance.transform.position = new Vector2(0.0f, i);
            beerUnitInstance.GetComponent<SpriteRenderer>().sprite = beerColorSprites[beerColorIndex];
            beerUnitInstance.tag = TagResolver.GetNameByIndex(beerColorIndex);
            BeerHandler beerHandler = beerUnitInstance.GetComponent<BeerHandler>();
            
            /* set callback for BeerSpawner which will notify 
             * the BeerSpawner (its parent), that the beer is destroyed */
            beerHandler.SetBeforeDestroyCallback(() =>
            {
                numberOfBeerUnitsLeftInTheRound--;
                if (numberOfBeerUnitsLeftInTheRound == 0)
                {
                    Debug.Log("All beer from this round destroyed");

                    // this is the moment where all beer units in this round are destroyed
                    GameManager.CheckIfLevelShouldEnd();
                }
            });
        }
        GameManager.UpdateBeersDispatched(numberOfBeerUnitsPerRound);

    }
}
