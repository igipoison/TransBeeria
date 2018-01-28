using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    private static int score = 0;
    private static int beersDispatched = 0;

    public static void UpdateBeersDispatched(int delta)
    {
        beersDispatched += delta;
        UpdateScoreText();

        if (beersDispatched >= 100)
        {
            GameFinished();
        }
    }

    public static void UpdateScore(int delta)
    {
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

    private static void GameFinished()
    {
        Debug.Log("GAME FINISHED. Score: " + score);
    }

	// Use this for initialization
	void Start () {
        RandomUtils.Initialize();
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
