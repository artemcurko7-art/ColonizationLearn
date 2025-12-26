using UnityEngine;

public static class UserUtils
{
    public static int GetRandomNumber(int minRange, int maxRange) =>
        Random.Range(minRange, maxRange);
}
