using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerHandler : MonoBehaviour {

    [Range(1, 20)]
    public int beerSpeed;

    private BeerTree beerTree;
    private Vector2 direction;
    private SwitchNode lastVisitedSwitchNode = null;
    private Node nodeToVisit = null;

    private Action beforeDestroyCallback;

    // Use this for initialization
    void Start () {

        beerTree = LevelLoader.getInstance().GetBeerTree();
        direction = new Vector2(0.0f, -1.0f);
        lastVisitedSwitchNode = beerTree.getRootNode();
    }
	
	// Update is called once per frame
	void Update () {

        if (!GameManager.IsGameRunning())
            return;

        if (this.IsOutOfCameraBounds())
        {
            DestroyAndNotify();
            return;
        }

        if (nodeToVisit == null)
        {
            nodeToVisit = lastVisitedSwitchNode.GetNextNode();           
        }
        
        if (nodeToVisit != null && (new Vector2(transform.position.x, transform.position.y) - nodeToVisit.coordinates).sqrMagnitude <= 0.1)
        {
            // check where to go
            this.transform.position = nodeToVisit.coordinates;

            if (nodeToVisit.GetType() == typeof(SwitchNode))
            {
                direction = ((SwitchNode)nodeToVisit).GetDirectionVector();
                lastVisitedSwitchNode = (SwitchNode)nodeToVisit;
                nodeToVisit = null;
            }
            else
            {
                BeerTag beerTag = TagResolver.GetTagByName(gameObject.tag);
                GameObject houseSpriteObj = GameObject.Find("HouseNode#" + nodeToVisit.nodeId);
                HouseHandler houseHandler = houseSpriteObj.GetComponent<HouseHandler>() as HouseHandler;
                BeerTag houseBeerTag = houseHandler.getBeerTag();

                if (beerTag == houseBeerTag)
                {
                    houseHandler.addLiter();
                    houseHandler.CheckAndReset();
                    GameManager.UpdateScore(1);
                }
                DestroyAndNotify();
            }
        }

        Vector2 translation = Time.deltaTime * beerSpeed * direction;
        this.transform.Translate(translation);
    }

    public void SetBeforeDestroyCallback(Action newBeforeDestroyCallback)
    {
        this.beforeDestroyCallback = newBeforeDestroyCallback;
    }

    private bool IsOutOfCameraBounds()
    {
        Camera mainCamera = Camera.main;
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = mainCamera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            mainCamera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));

        return (this.transform.position.x > bounds.center.x + bounds.extents.x ||
                this.transform.position.x < bounds.center.x - bounds.extents.x ||
                this.transform.position.y < bounds.center.y - bounds.extents.y);
    }

    private void DestroyAndNotify()
    {
        this.beforeDestroyCallback();
        Destroy(this.gameObject);
    }
}
