using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    private static LevelLoader instance = null;
	private static Camera m_MainCamera;

    public GameObject housePrefab;
    public GameObject pipeHorizontal;
    public GameObject pipeVertical;
    public GameObject switchableObject;
    public GameObject rootObject;
    private static BeerTree beerTree;
	//private int[] levelCameraSize;
	private float lastSwitchYcoordinate;

    public static LevelLoader getInstance()
    {
        if (instance == null)
            instance = new LevelLoader();

        return instance;
    }

    public static void Subscribe(SwitchHandler s)
    {
        s.click += new SwitchHandler.ClickHandler(ChangeState);
    }

    private static void ChangeState(SwitchHandler switchHandler, EventArgs e)
    {
        beerTree.SetNodeState(switchHandler.name, switchHandler.currentIndex);
    }

    // Use this for initialization
    void Start () {

        BeerTree bT = MapGenerator(StartGame.getLevel());


        GenerateSprites(bT);
        GeneratePipes(bT);

        beerTree = bT;
		//levelCameraSize = new int[4]{ 2, 3, 4, 5 };

		SetMainCamera(1f,2f);

    }


	private void SetMainCamera(float yAxisCorrection,float sizeCorrection)
	{
		m_MainCamera = Camera.main;

		//alternativly we can use fixed camera size per level
		//m_MainCamera.orthographicSize = levelCameraSize[StartGame.getLevel()];
		m_MainCamera.orthographicSize = (Math.Abs(lastSwitchYcoordinate)/2) + sizeCorrection;
		float newY = -m_MainCamera.orthographicSize + yAxisCorrection;
		Vector3 newPosition = new Vector3 (0.0f, newY,-10.0f);
		m_MainCamera.transform.position = newPosition;
	}

    public BeerTree GetBeerTree()
    {
        return beerTree;
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
            houseSprite.name = "HouseNode#" + node.nodeId;
            houseSprite.transform.position = node.coordinates;
        }
        else
        {
            var isRoot = ((SwitchNode)node).isRoot;
            var switchPrefab = Instantiate(isRoot? rootObject : switchableObject) as GameObject;

            var handler = switchPrefab.GetComponent<SwitchHandler>();

            switchPrefab.name =  node.nodeId;
            if(!isRoot)
            ((SwitchNode)node).switchObject.state = StateFactory.Create(handler.currentIndex);
            if (handler != null)
            {
                Subscribe(handler);
            }
            switchPrefab.transform.position = node.coordinates;
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

    public List<HouseHandler> GetAllHouseHandlers()
    {
        List<Node> houseNodes = beerTree.getAllNodes().FindAll((Node node) =>
        {
            return node.GetType() == typeof(HouseNode);
        });

        List<HouseHandler> houseHandlers = new List<HouseHandler>();
        foreach (Node houseNode in houseNodes)
        {
            GameObject houseSpriteObj = GameObject.Find("HouseNode#" + houseNode.nodeId);
            houseHandlers.Add(houseSpriteObj.GetComponent<HouseHandler>());
        }
        return houseHandlers;
    }

    BeerTree MapGenerator(int numberOfSwitches)
    {
        SwitchNode parentSwitch = new SwitchNode(new Vector2(0.0f, 0.0f), "SWITCH_ROOT", new Switch(), true);
        BeerTree bT = new BeerTree(parentSwitch);

        int numOfSwitches = numberOfSwitches;

        float y = 0.0f;
        int idSwitchCounter = 1;
        int numOfHouses = 0;
        int idHouseCounter = 1;

        for (int i = 0; i < numOfSwitches; i++)
        {
			y = y - 3;
            SwitchNode newSwitch = new SwitchNode(new Vector2(0.0f, y), "SWITCH_" + idSwitchCounter++, new Switch(), false);
			if (i == numOfSwitches - 1)
				lastSwitchYcoordinate = y;
            
            numOfHouses = RandomUtils.GetRandomNumber(1, 4);

            if (i == 0)
            {
                bT.AddSwitch(newSwitch, "SWITCH_ROOT");
            }
            else
            {
                bT.AddSwitch(newSwitch, parentSwitch.nodeId);
            }
            parentSwitch = newSwitch;

            switch (numOfHouses)
            {
                case 1:
                    {
                        HouseNode leftHouse = new HouseNode(new House(), new Vector2(-5.0f, newSwitch.coordinates.y), "HOUSE_" + idHouseCounter++);
                        bT.AddHouse(leftHouse, newSwitch.nodeId);
                        break;
                    }
                case 2:
                    {
                        HouseNode rightHouse = new HouseNode(new House(), new Vector2(5.0f, newSwitch.coordinates.y), "HOUSE_" + idHouseCounter++);
                        bT.AddHouse(rightHouse, newSwitch.nodeId);
                        break;
                    }

                default:
                    {
                        HouseNode leftHouse = new HouseNode(new House(), new Vector2(-5.0f, newSwitch.coordinates.y), "HOUSE_" + idHouseCounter++);
                        bT.AddHouse(leftHouse, newSwitch.nodeId);
                        HouseNode rightHouse = new HouseNode(new House(), new Vector2(5.0f, newSwitch.coordinates.y), "HOUSE_" + idHouseCounter++);
                        bT.AddHouse(rightHouse, newSwitch.nodeId);
                        break;
                    }

            }


        }

        return bT;

    }
}
