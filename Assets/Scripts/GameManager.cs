using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isShopOpen = false;
    public static int coin = 0, sumCoin = 0, engineType = 4, wheelType = 0;
    public static int[] wheelItemList = new int[3] { 0, 0, 0 }, engineItemList = new int[2] { 0, 0 };
    private void Start()
    {
        isShopOpen = false;
    }
}
