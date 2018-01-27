using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHandler : MonoBehaviour {

    public Sprite[] switches;
    public Sprite switchHighlight;
    private GameObject objectHighlight;
    public int currentIndex;
    public EventArgs e = null;
    public event ClickHandler click;
    public delegate void ClickHandler(SwitchHandler switchHandler, EventArgs e);
    private int numberOfSwitches=3;
	// Use this for initialization
	void Start () {

        currentIndex = 0;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = switches[currentIndex];

        objectHighlight = new GameObject();
        objectHighlight.transform.position = this.gameObject.transform.position;
        objectHighlight.AddComponent<SpriteRenderer>();
        objectHighlight.GetComponent<SpriteRenderer>().sprite = switchHighlight;
        objectHighlight.GetComponent<SpriteRenderer>().color = Color.clear;
        objectHighlight.GetComponent<SpriteRenderer>().sortingLayerName = "HousesLayer";
    }
	
    void OnMouseDown()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = getNextSprite();
        click(this, e);
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
        currentIndex++;
        Sprite result = switches[(currentIndex) % numberOfSwitches];
        currentIndex = (currentIndex) % numberOfSwitches;
        return result;
    }

    void Update () {

    }
}
