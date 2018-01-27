using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    

	// Use this for initialization
	void Start () {

        Vector2 coordinatesRoot = new Vector2(0.0f,0.0f);
        string nodeIdSwitchRoot = "SWITCH_ROOT";
        Switch switchRoot = new Switch();
        SwitchNode switchNodeRoot = new SwitchNode(coordinatesRoot, nodeIdSwitchRoot, switchRoot);
        
        Vector2 coordinatesOne = new Vector2(0.0f, -10.0f);
        string nodeIdSwitchOne = "SWITCH_ONE";
        Switch switchOne = new Switch();
        SwitchNode switchNodeOne = new SwitchNode(coordinatesOne, nodeIdSwitchOne, switchOne);

        Vector2 coordinatesTwo = new Vector2(0.0f, -20.0f);
        string nodeIdSwitchTwo = "SWITCH_TWO";
        Switch switchTwo = new Switch();
        SwitchNode switchNodeTwo = new SwitchNode(coordinatesTwo, nodeIdSwitchTwo, switchTwo);

        Vector2 coordinatesHouseOne = new Vector2(-10.0f, -10.0f);
        string nodeIdHouseOne = "HOUSE_ONE";
        HouseNode houseNodeOne = new HouseNode(coordinatesHouseOne, nodeIdHouseOne);

        Vector2 coordinatesHouseTwo = new Vector2(10.0f, -10.0f);
        string nodeIdHouseTwo = "HOUSE_TWO";
        HouseNode houseNodeTwo = new HouseNode(coordinatesHouseTwo, nodeIdHouseTwo);

        Vector2 coordinatesHouseThree = new Vector2(10.0f, -20.0f);
        string nodeIdHouseThree = "HOUSE_THREE";
        HouseNode houseNodeThree = new HouseNode(coordinatesHouseThree, nodeIdHouseThree);

        BeerTree bT = new BeerTree(switchNodeRoot);
        bT.AddSwitch(switchNodeOne, nodeIdSwitchRoot);
        bT.AddSwitch(switchNodeTwo, nodeIdSwitchOne);
        bT.AddHouse(houseNodeOne, nodeIdSwitchOne);
        bT.AddHouse(houseNodeTwo, nodeIdSwitchOne);
        bT.AddHouse(houseNodeThree, nodeIdSwitchTwo);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
