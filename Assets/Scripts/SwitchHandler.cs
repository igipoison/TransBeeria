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
        //this.gameObject.GetComponent<SpriteRenderer>().sprite = switchHighlight;

        objectHighlight.GetComponent<SpriteRenderer>().color = Color.white;
        //this.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan; // RGBA(107.000, 33.000, 152.000, 1.000);
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is on GameObject.");
        //this.gameObject.GetComponent<SpriteRenderer>().sprite = getNextSprite();
    }

    void OnMouseExit()
    {
        objectHighlight.GetComponent<SpriteRenderer>().color = Color.clear;

        //this.gameObject.GetComponent<SpriteRenderer>().color = this.gameObject.GetComponent<SpriteRenderer>().color;
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }

    Sprite getNextSprite()
    {
        Sprite result = switches[(currentIndex++) % numberOfSwitches];
        return result;
    }

    void Update () {

    }
}
