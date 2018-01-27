using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node {

    public Vector2 coordinates;
    public string nodeId;

    protected Node(Vector2 coordinates, string nodeId)
    {
        this.coordinates = coordinates;
        this.nodeId = nodeId;
    }
}
