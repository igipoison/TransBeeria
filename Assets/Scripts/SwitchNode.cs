using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class SwitchNode : Node
    {
        public Switch switchObject;
        public List<Node> childs;
        public bool isRoot;

        public SwitchNode(Vector2 coordinates, string nodeId, Switch s, bool isRoot = false) : base(coordinates, nodeId)
        {
            childs = new List<Node>();
            this.switchObject = s;
            this.isRoot = isRoot;
        }
    }
}
