using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isShopOpen = false;
    public static int coin = 100000000, sumCoin = 0, engineType = 4, wheelType = 4, sceneIndex = 0;
    public static int[] wheelItemList = new int[3] { 0, 0, 0 }, engineItemList = new int[2] { 0, 0 };
    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        isShopOpen = false;
    }
}
