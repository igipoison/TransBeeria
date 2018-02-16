using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text TextScore;
    public Text TextTimeUntilNextBeer;
    public Text TextLevel;
    public Text TextOverallScore;
    public Text TextRoundsLeft;
    public GameObject PanelLevelEnding;

	// Use this for initialization
	void Start ()
    {
      
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void Initialize(int timeUntilNextRound, int totalBeerToDispatch, int beerUnitsInOneRound, int roundsLeft)
    {
        ShowLevelEndingPanel(false);
        UpdateScore(0, totalBeerToDispatch);
        UpdateNextRoundInfo(timeUntilNextRound, beerUnitsInOneRound);
        UpdateRoundsLeft(roundsLeft);
    }

    public void UpdateScore(int score, int beersDispatched)
    {
        TextScore.text = "Score: " + score + " / " + beersDispatched;
    }

    public void UpdateNextRoundInfo(int timeUntilNextBeer, int beerUnitsInNextRound)
    {
        TextTimeUntilNextBeer.text = "Time until next beer: " + timeUntilNextBeer + "  (" + beerUnitsInNextRound + " liters)";
    }

    public void UpdateOverallScore(int score, int total)
    {
        TextOverallScore.text = score + " / " + total;
    }

    public void UpdateRoundsLeft(int roundsLeft)
    {
        TextRoundsLeft.text = "Rounds left: " + roundsLeft;
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
