using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class HouseNode : Node
    {
        public House houseObject;
        public HouseNode(House h,Vector2 coordinates, string nodeId) : base(coordinates, nodeId)
        {
            houseObject = h;
        }

        public override Vector2 HandleBeerEnterance(GameObject gameObject)
        {
           return this.houseObject.DestroyArrivedBeer(gameObject);
        }
    }
}
