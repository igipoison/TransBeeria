using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text TextScore;
    public Text TextTimeUntilNextBeer;
    public Text TextLevel;
    public Text TextOverallScore;
    public GameObject PanelLevelEnding;

	// Use this for initialization
	void Start ()
    {
        Initialize();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void Initialize()
    {
        ShowLevelEndingPanel(false);
        UpdateScore(0, 0);
        UpdateTimeUntilNextBeer(0);
    }

    public void UpdateScore(int score, int beersDispatched)
    {
        TextScore.text = "Score: " + score + " / " + beersDispatched;
    }

    public void UpdateTimeUntilNextBeer(int timeUntilNextBeer)
    {
        TextTimeUntilNextBeer.text = "Time until next beer: " + timeUntilNextBeer;
    }

    public void UpdateOverallScore(int score, int total)
    {
        TextOverallScore.text = score + " / " + total;
    }

    public void UpdateLevel(int level)
    {
        TextLevel.text = "Level: " + level;
    }
    
    public void ShowLevelEndingPanel(bool show)
    {
        PanelLevelEnding.gameObject.SetActive(show);
    }
}
