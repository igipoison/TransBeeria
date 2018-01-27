using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHandler : MonoBehaviour {

    public Sprite[] switches;
    public int currentIndex;
    private int numberOfSwitches=3;
	// Use this for initialization
	void Start () {

        System.Random r = new System.Random(DateTime.Now.Millisecond);
        currentIndex = r.Next(3);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = getNextSprite();
    }
	
    void OnMouseDown()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = getNextSprite();
    }

    Sprite getNextSprite()
    {
        Sprite result = switches[(currentIndex++) % numberOfSwitches];
        return result;
    }

    void Update () {

    }
}
