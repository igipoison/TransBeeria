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
        
    }
	
	
    void OnMouseDown()
    {
        Debug.Log(this.gameObject.name);
        Sprite currentSwitch = switches[(currentIndex++) % numberOfSwitches];
        this.gameObject.GetComponent<SpriteRenderer>().sprite = currentSwitch;
    }

    void Update () {

    }
}
