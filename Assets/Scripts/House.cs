using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{

    public class House : MonoBehaviour
    {
        //public Sprite house;

        public Vector2 DestroyArrivedBeer(GameObject beer)
        {
            Destroy(beer);
            return new Vector2(0.0f, 0.0f);
        }

    }
}
