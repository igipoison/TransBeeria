using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseHandler : MonoBehaviour
{

    [Range(1, 20)]
    public float askForBeerInterval = 10.0f;
    [Range(1, 20)]
    public float askForBeerStartValue = 0.0f;
    [Range(1, 100)]
    public int demandingLiters = 10;

    public Sprite[] beerSprites;

    private BeerTag beerType = BeerTag.STOUT;
    private float internalBeerInterval;
    private float currentBeerLitersGoal = 0;
    private int currentLiters = 0;

    private System.Random random;

    // Use this for initialization
    void Start()
    {
        internalBeerInterval = askForBeerStartValue;
        random = new System.Random(System.DateTime.Now.Millisecond);
    }

    // Update is called once per frame
    void Update()
    {
        if (internalBeerInterval > askForBeerInterval)
        {
            internalBeerInterval = 0.0f;

            if (currentLiters >= currentBeerLitersGoal)
                AskForBeer();
        }
        else
        {
            internalBeerInterval += Time.deltaTime;
        }
    }

    void AskForBeer()
    {
        currentBeerLitersGoal = demandingLiters;
        beerType = (BeerTag)random.Next(3);
        GameObject beerNotificationSpriteGO = new GameObject();
        beerNotificationSpriteGO.AddComponent<SpriteRenderer>();
        beerNotificationSpriteGO.GetComponent<SpriteRenderer>().sprite = beerSprites[(int)beerType];
        beerNotificationSpriteGO.GetComponent<SpriteRenderer>().sortingLayerName = "NotificationsLayer";
        beerNotificationSpriteGO.transform.position = this.gameObject.transform.position + new Vector3(0.5f, 0.5f);
    }

    void OnGUI()
    {
        if (currentBeerLitersGoal > 0)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 20;
            Vector3 getPixelPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.0f, 2.0f));
            getPixelPos.y = Screen.height - getPixelPos.y;
            GUI.Label(new Rect(getPixelPos.x, getPixelPos.y, 100, 50), "" + currentLiters + " / " + currentBeerLitersGoal, style );
        }
       
    }

    public void addLiter()
    {
        currentLiters++;
    }

    public BeerTag getBeerTag()
    {
        return beerType;
    }
}
