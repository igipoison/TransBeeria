using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static StartGame levelChanger;
    private static int score;
    private static int beersDispatched;
    private static int roundsLeft;
    private static bool gameRunning;
    private static UIController uiController;
    
    public static void InitializeState()
    {
        score = 0;
        beersDispatched = 0;
        gameRunning = false;
        roundsLeft = GetTotalNumberOfRounds();
    }

    public static void CheckIfLevelShouldEnd()
    {
        if (beersDispatched >= GetTotalNumberOfDispatchBeerForThisLevel())
        {
            uiController.UpdateOverallScore(score, beersDispatched);
            uiController.ShowLevelEndingPanel(true);
            gameRunning = false;
        }
    }

    public static void UpdateBeersDispatched(int delta)
    {
        beersDispatched += delta;
        UpdateScoreText();
    }

    public static void UpdateScore(int delta)
    {
        Debug.Log("update score");
        score += delta;
        UpdateScoreText();
    }

    public static void UpdateNextRoundInfo(int timeUntilNextBeer, int beerUnitsInNextRound)
    {
        uiController.UpdateNextRoundInfo(timeUntilNextBeer, beerUnitsInNextRound);
    }

    public static void DecreaseRoundsLeft()
    {
        roundsLeft--;
        uiController.UpdateRoundsLeft(roundsLeft);
    }

    public static void FinishGame()
    {
        levelChanger.StartNewLevel();
    }

    public static bool IsGameRunning()
    {
        return gameRunning;
    }

    private static void UpdateScoreText()
    {
        uiController.UpdateScore(score, GetTotalNumberOfDispatchBeerForThisLevel());
    }

    private static int GetTotalNumberOfRounds()
    {
        return StartGame.getLevel() * 2;
    }

    private static int GetTotalNumberOfDispatchBeerForThisLevel()
    {
        return GetTotalNumberOfRounds() * 10;
    }

	// Use this for initialization
	void Start () {

        InitializeState();

        uiController = FindObjectOfType<UIController>();
        uiController.Initialize(0, GetTotalNumberOfDispatchBeerForThisLevel(), 10, roundsLeft);
        uiController.UpdateLevel(StartGame.getLevel());

        RandomUtils.Initialize();
        levelChanger = GetComponent<StartGame>();

        gameRunning = true;
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
