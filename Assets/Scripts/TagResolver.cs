using UnityEngine;
using System.Collections;

public enum BeerTag
{
    UKNOWN = -1,
    STOUT = 0,
    RED = 1,
    PILSNER = 2
}

public class TagResolver {


    public static string GetBeerNameByTag(BeerTag beerTag)
    {
        switch(beerTag)
        {
            case BeerTag.STOUT: return "STOUT";
            case BeerTag.RED: return "RED";
            case BeerTag.PILSNER: return "PILSNER";
            default: return "?";
        }
    }

    public static string GetNameByIndex(int index)
    {
        BeerTag beerTag = (BeerTag)index;
        return GetBeerNameByTag(beerTag);
    }

    public static int GetIndexByTag(BeerTag beerTag)
    {
        return (int)beerTag;
    }

    public static BeerTag GetTagByName(string name)
    {
        switch (name)
        {
            case "STOUT": return BeerTag.STOUT;
            case "RED": return BeerTag.RED;
            case "PILSNER": return BeerTag.PILSNER;
            default: return BeerTag.UKNOWN;
        }
    }
}
