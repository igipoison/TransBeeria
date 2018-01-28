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
        (FindObjectOfType<Text>() as Text).text = "Score: " + score;
    }

	// Use this for initialization
	void Start () {
        RandomUtils.Initialize();
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
