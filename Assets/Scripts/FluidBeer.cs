using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick
{
    private BeerBubble mBubbleBeer1;
    private BeerBubble mBubbleBeer2;
    float mDistanceConstraint;

    public Stick(GameObject beerBubbleGO1, GameObject beerBubbleGO2)
    {
        this.mBubbleBeer1 = beerBubbleGO1.GetComponent<BeerBubble>();
        this.mBubbleBeer2 = beerBubbleGO2.GetComponent<BeerBubble>();
        this.mDistanceConstraint = (mBubbleBeer2.GetPosition() - mBubbleBeer1.GetPosition()).magnitude;
    }

    public void Update(float deltaTime)
    {
        Vector3 directionVector = mBubbleBeer2.GetPosition() - mBubbleBeer1.GetPosition();
        float currentLength = directionVector.magnitude;
        float distanceToTravel = (currentLength - mDistanceConstraint) / 2.0f;
        Vector3 directionNormalized = directionVector.normalized;

        Vector3 deltaVector = directionNormalized * (distanceToTravel / currentLength);
        
        this.mBubbleBeer1.SetPosition(this.mBubbleBeer1.GetPosition() + deltaVector);
        this.mBubbleBeer2.SetPosition(this.mBubbleBeer2.GetPosition() - deltaVector);
    }
}
public class FluidBeer : MonoBehaviour {

    public GameObject beerBubble;
    public int noOfBubblesPerSide = 2;
    public float bubblesDistance = 1.0f;

    private List<Stick> mSticks;
    private GameObject[,] mNodes;

    void Start()
    {
        initializeNodes();
        initializeSticks();
    }

    private void initializeNodes()
    {
        Vector3 INITIAL_PREV_POS_OFFSET = new Vector3(0.2f, 0.2f, 0.0f);

        mNodes = new GameObject[noOfBubblesPerSide, noOfBubblesPerSide];
        for (int i = 0; i < noOfBubblesPerSide; i++)
        {
            for (int j = 0; j < noOfBubblesPerSide; j++)
            {
                GameObject newBubbleGO = Instantiate(beerBubble);
                BeerBubble newBubble = newBubbleGO.GetComponent<BeerBubble>();
                Vector3 initialBubblePosition = new Vector3(i * bubblesDistance, j * bubblesDistance);
                newBubble.Initialize(initialBubblePosition - INITIAL_PREV_POS_OFFSET, initialBubblePosition);
                mNodes[i, j] = newBubbleGO;
            }
        }
    }

    private void initializeSticks()
    {
        mSticks = new List<Stick>();
        for (int i = 0; i < noOfBubblesPerSide; i++)
        {
            for (int j = 0; j < noOfBubblesPerSide; j++)
            {
                if (j + 1 < noOfBubblesPerSide)
                    mSticks.Add(new Stick(mNodes[i, j], mNodes[i, j + 1]));
                if (i + 1 < noOfBubblesPerSide)
                    mSticks.Add(new Stick(mNodes[i, j], mNodes[i + 1, j]));
            }
        }
    }

    void Update()
    {
        updateSpecialPoints(Time.deltaTime);
        UpdateSticks(Time.deltaTime);
    }

    private void updateSpecialPoints(float deltaTime)
    {
        for (int i = 0; i < noOfBubblesPerSide; i++)
        {
            for (int j = 0; j < noOfBubblesPerSide; j++)
            {
                if (mNodes[i, j] != null)
                    mNodes[i, j].GetComponent<BeerBubble>().UpdateFromOutside(deltaTime);
            }
        }
    }

    private void UpdateSticks(float deltaTime)
    {
        foreach (Stick stick in mSticks)
        {
            stick.Update(deltaTime);
        }
    }
}
