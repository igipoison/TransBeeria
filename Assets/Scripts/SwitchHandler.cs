using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHandler : MonoBehaviour {

    public Sprite[] switches;
    public Sprite switchHighlight;
    private GameObject objectHighlight;
    public int currentIndex;
    private int numberOfSwitches=3;
	// Use this for initialization
	void Start () {

        System.Random r = new System.Random(DateTime.Now.Millisecond);
        currentIndex = r.Next(3);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = getNextSprite();

        objectHighlight = new GameObject();
        objectHighlight.transform.position = this.gameObject.transform.position;
        objectHighlight.AddComponent<SpriteRenderer>();
        objectHighlight.GetComponent<SpriteRenderer>().sprite = switchHighlight;
        objectHighlight.GetComponent<SpriteRenderer>().color = Color.clear;
    }
	
    void OnMouseDown()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = getNextSprite();
    }

    void OnMouseOver()
    {
        objectHighlight.GetComponent<SpriteRenderer>().color = Color.white;
    }

    void OnMouseExit()
    {
        objectHighlight.GetComponent<SpriteRenderer>().color = Color.clear;
    }

    Sprite getNextSprite()
    {
        Sprite result = switches[(currentIndex++) % numberOfSwitches];
        return result;
    }

    void Update () {

    }
}
