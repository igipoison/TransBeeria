using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class StateLeft : IState
    {
        public Vector2 direction {get; set;}

        public StateLeft()
        {
            direction = new Vector2(-1.0f, 0.0f);
        }

        public Node GetNextNode(List<Node> childs, Vector2 coordinates)
        {
           return childs.Find(childNode => childNode.coordinates.x < coordinates.x);
        }
    }
}
