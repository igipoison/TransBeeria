using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    private static int score = 0;

    public static Game instance = null;

    public static Game getInstance()
    {
        if (instance == null)
            instance = new Game();

        return instance;
    }

    public static void UpdateScore(int delta)
    {
        score += delta;
        (GameObject.Find("TextScore").GetComponent<Text>() as Text).text = "Score: " + score;
    }

    public static void UpdateTimeUntilNextBeer(int timeUntilNextBeer)
    {
        (GameObject.Find("TextTimeUntilBeer").GetComponent<Text>() as Text).text = "Time until next beer: " + timeUntilNextBeer;
    }

	// Use this for initialization
	void Start () {
        RandomUtils.Initialize();
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
