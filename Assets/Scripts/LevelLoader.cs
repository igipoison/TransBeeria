using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {


    public GameObject housePrefab;
    public GameObject pipeHorizontal;
    public GameObject pipeVertical;
    public GameObject switchableObject;
    public GameObject rootObject;
    public GameObject beerUnit;
    

    public void Subscribe(SwitchHandler s)
    {
        s.click += new SwitchHandler.ClickHandler(HeardIt);
    }

    private void HeardIt(SwitchHandler switchHandler, EventArgs e)
    {
        Debug.Log(switchHandler.currentIndex + ";" + switchHandler.gameObject.name);
    }

    // Use this for initialization
    void Start () {

        Vector2 coordinatesRoot = new Vector2(0.0f,0.0f);
        string nodeIdSwitchRoot = "SWITCH_ROOT";
        Switch switchRoot = new Switch();
        SwitchNode switchNodeRoot = new SwitchNode(coordinatesRoot, nodeIdSwitchRoot, switchRoot,true);
        
        Vector2 coordinatesOne = new Vector2(0.0f, -5.0f);
        string nodeIdSwitchOne = "SWITCH_ONE";
        Switch switchOne = new Switch();
        SwitchNode switchNodeOne = new SwitchNode(coordinatesOne, nodeIdSwitchOne, switchOne);

        Vector2 coordinatesTwo = new Vector2(0.0f, -10.0f);
        string nodeIdSwitchTwo = "SWITCH_TWO";
        Switch switchTwo = new Switch();
        SwitchNode switchNodeTwo = new SwitchNode(coordinatesTwo, nodeIdSwitchTwo, switchTwo);

        Vector2 coordinatesHouseOne = new Vector2(-5.0f, -5.0f);
        string nodeIdHouseOne = "HOUSE_ONE";
        HouseNode houseNodeOne = new HouseNode(coordinatesHouseOne, nodeIdHouseOne);

        Vector2 coordinatesHouseTwo = new Vector2(5.0f, -5.0f);
        string nodeIdHouseTwo = "HOUSE_TWO";
        HouseNode houseNodeTwo = new HouseNode(coordinatesHouseTwo, nodeIdHouseTwo);

        Vector2 coordinatesHouseThree = new Vector2(5.0f, -10.0f);
        string nodeIdHouseThree = "HOUSE_THREE";
        HouseNode houseNodeThree = new HouseNode(coordinatesHouseThree, nodeIdHouseThree);

        BeerTree bT = new BeerTree(switchNodeRoot);
        bT.AddSwitch(switchNodeOne, nodeIdSwitchRoot);
        bT.AddSwitch(switchNodeTwo, nodeIdSwitchOne);
        bT.AddHouse(houseNodeOne, nodeIdSwitchOne);
        bT.AddHouse(houseNodeTwo, nodeIdSwitchOne);
        bT.AddHouse(houseNodeThree, nodeIdSwitchTwo);

        GenerateSprites(bT);
        GeneratePipes(bT);

        var switchSprite = Instantiate(beerUnit) as GameObject;
        switchSprite.transform.position = new Vector2(0.0f, -1.0f);
    }
	
    void GenerateSprites(BeerTree bt)
    {
        SwitchNode startNode = bt.start;
        GenerateSprite(startNode);
    }

    void GenerateSprite(Node node)
    {
        if (node.GetType() == typeof(HouseNode))
        {
            var houseSprite = Instantiate(housePrefab) as GameObject;

            houseSprite.transform.position = node.coordinates;
        }
        else
        {
            var isRoot = ((SwitchNode)node).isRoot;
            var switchSprite = Instantiate(isRoot? rootObject : switchableObject) as GameObject;
            var handler = switchSprite.GetComponent<SwitchHandler>();
            if(handler!=null)
            this.Subscribe(handler);
            switchSprite.transform.position = node.coordinates;
            (node as SwitchNode).childs.ForEach((Node switchNodeChild) =>
            {
                GenerateSprite(switchNodeChild);
            });
        }
    }

    void GeneratePipes(BeerTree bt)
    {
        SwitchNode startNode = bt.start;
        GeneratePipes(startNode);
    }

    void GeneratePipes(SwitchNode switchNode)
    {
        switchNode.childs.ForEach((Node child) =>
        {
            float xdiff = Mathf.Abs( switchNode.coordinates.x - child.coordinates.x);
            float ydiff = Mathf.Abs(switchNode.coordinates.y - child.coordinates.y);
            int xcoeff = switchNode.coordinates.x - child.coordinates.x > 0 ? -1 : 1;
            int ycoeff = switchNode.coordinates.y - child.coordinates.y > 0 ? -1 : 1;

            for (int i = 1; i < xdiff; i++)
            {
                var horizontalPipe = Instantiate(pipeHorizontal) as GameObject;
                horizontalPipe.transform.position = new Vector2(switchNode.coordinates.x + i* xcoeff, child.coordinates.y);
            }

            for (int i = 1; i < ydiff; i++)
            {
                var horizontalPipe = Instantiate(pipeVertical) as GameObject;
                horizontalPipe.transform.position = new Vector2( child.coordinates.x, switchNode.coordinates.y + i * ycoeff);
            }

            if (child.GetType()==typeof(SwitchNode))
            GeneratePipes((SwitchNode)child);
        });
    }

    // Update is called once per frame
    void Update () {
		
	}
}
