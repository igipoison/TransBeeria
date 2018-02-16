using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    private static StartGame levelChanger;
    private static int score = 0;
    private static int beersDispatched = 0;
    private static bool gameRunning = false;
    private static GameObject panelLevelEnding;

    public static void Reset()
    {
        score = 0;
        beersDispatched = 0;
    }

    public static void UpdateBeersDispatched(int delta)
    {
        beersDispatched += delta;
        UpdateScoreText();

        if (beersDispatched > StartGame.getLevel() * 2 * 10)
        {
            panelLevelEnding.gameObject.SetActive(true);
            gameRunning = false;
        }
    }

    public static void UpdateScore(int delta)
    {
        Debug.Log("update score");
        score += delta;
        UpdateScoreText();
    }

    public static void UpdateTimeUntilNextBeer(int timeUntilNextBeer)
    {
        (GameObject.Find("TextTimeUntilBeer").GetComponent<Text>() as Text).text = "Time until next beer: " + timeUntilNextBeer;
    }

    private static void UpdateScoreText()
    {
        (GameObject.Find("TextScore").GetComponent<Text>() as Text).text = "Score: " + score + " / " + beersDispatched;
    }

    public static void GameFinished()
    {
        Reset();
        levelChanger.StartNewLevel();
        Debug.Log("GAME FINISHED. Score: " + score);
    }

    public static bool IsGameRunning()
    {
        return gameRunning;
    }

	// Use this for initialization
	void Start () {
        RandomUtils.Initialize();
        levelChanger = GetComponent<StartGame>();
        string levelName;
        if (StartGame.getLevel() < 4) levelName = StartGame.getLevel() + "";
        else levelName = "God";

        (GameObject.Find("TextLevel").GetComponent<Text>() as Text).text = "Level: " + levelName;
        panelLevelEnding = GameObject.Find("PanelLevelEnding");
        panelLevelEnding.gameObject.SetActive(false);

        gameRunning = true;
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
