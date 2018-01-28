using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class StateDown : IState
    {
        public Vector2 direction { get; set; }

        public StateDown()
        {
            direction = new Vector2(0.0f, -1.0f);
        }

        public Node GetNextNode(List<Node> childs, Vector2 coordinates)
        {
           return childs.Find(childNode => childNode.coordinates.y < coordinates.y);
        }
    }
}
