using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerHandler : MonoBehaviour {

    [Range(1, 20)]
    public int beerSpeed;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        float translation = Time.deltaTime * beerSpeed;
        this.transform.Translate(new Vector3(0, -translation));
    }
}
