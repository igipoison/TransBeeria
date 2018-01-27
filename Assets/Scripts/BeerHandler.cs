using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerHandler : MonoBehaviour {

    [Range(1, 20)]
    public int beerSpeed;

    private BeerTree beerTree;
    private Vector2 direction;
    private SwitchNode lastVisitedNode = null;
    private Node nodeToVisit = null;

    // Use this for initialization
    void Start () {

        beerTree = LevelLoader.getInstance().GetBeerTree();
        direction = new Vector2(0.0f, -1.0f);
    }
	
	// Update is called once per frame
	void Update () {

        if (lastVisitedNode == null)
        {
            lastVisitedNode = beerTree.getRootNode();
        }

        if (nodeToVisit == null)
        { 
            int lastVisitedNodeState = lastVisitedNode.switchObject.state;
            if (lastVisitedNodeState == 0) // left
            {
                nodeToVisit = lastVisitedNode.childs.Find(childNode => childNode.coordinates.x < lastVisitedNode.coordinates.x);
            }
            else if (lastVisitedNodeState == 1) // right 
            {
                nodeToVisit = lastVisitedNode.childs.Find(childNode => childNode.coordinates.x > lastVisitedNode.coordinates.x);
            }
            else // down
            {
                nodeToVisit = lastVisitedNode.childs.Find(childNode => childNode.coordinates.y < lastVisitedNode.coordinates.y);
            }
        }
        
        if (nodeToVisit != null && (new Vector2(transform.position.x, transform.position.y) - nodeToVisit.coordinates).sqrMagnitude <= 0.1)
        {
            // check where to go
            this.transform.position = nodeToVisit.coordinates;
            
            if (nodeToVisit.GetType() == typeof(SwitchNode))
            {
                // we can proceed
                SwitchNode switchNodeToVisit = nodeToVisit as SwitchNode;
                if (switchNodeToVisit.switchObject.state == 0) // left
                {
                    direction = new Vector2(-1.0f, 0.0f);
                }
                else if (switchNodeToVisit.switchObject.state == 1) // right
                {
                    direction = new Vector2(1.0f, 0.0f);
                }
                else // down
                {
                    direction = new Vector2(0.0f, -1.0f);
                }

                lastVisitedNode = switchNodeToVisit;
                nodeToVisit = null;
                
            }
            else if (nodeToVisit.GetType() == typeof(HouseNode))
            {
                // we are done
                // TO-DO - count score
                Destroy(this.gameObject);
               
            }
        }


        Vector2 translation = Time.deltaTime * beerSpeed * direction;
        this.transform.Translate(translation);
    }
}
