using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerBubble : MonoBehaviour {

    private Rigidbody2D mRigidBody;
    private Vector3 mPrevPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 v = collision.relativeVelocity;
    }

    public void UpdateFromOutside(float deltaTime)
    {
        Vector3 velocity = this.transform.position - mPrevPosition;
        Vector3 gravityVec = new Vector3(0, -0.0981f, 0.0f);
        this.SetPosition(this.GetPosition() + (velocity + gravityVec));
    }

    public void Initialize(Vector3 prevPosition, Vector3 newPosition)
    {
        this.mPrevPosition = prevPosition;
        this.transform.position = newPosition;
    }

    public void SetPosition(Vector3 newPosition)
    {
        mPrevPosition = this.transform.position;
        this.transform.position = newPosition;
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }
}
