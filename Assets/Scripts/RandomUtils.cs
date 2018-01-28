using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public static class RandomUtils
{
    private static RNGCryptoServiceProvider random;
    
    public static void Initialize()
    {
        random = new RNGCryptoServiceProvider();
    }

    public static int GetRandomNumber(int minValue, int maxValue)
    {
        byte[] bytes = new byte[1];
        random.GetBytes(bytes);
        int result = minValue + bytes[0] % (maxValue - minValue);
        return result;
    }
}
