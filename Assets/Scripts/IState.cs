using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IState
    {
        Vector2 direction { get; set; }
        Node GetNextNode(List<Node> childs,Vector2 coordinates);
    }
}
