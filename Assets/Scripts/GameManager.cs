using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isShopOpen = false, isCheatShopOpen = false, isCheat = false;
    public static int coin = 100000000, sumCoin = 0, engineType = 4, wheelType = 4, sceneIndex = 0;
    public static int[] wheelItemList = new int[3] { 0, 0, 0 }, engineItemList = new int[2] { 0, 0 }, clear = new int[3] { 1,0,0};

    [SerializeField] private GameObject cheatShop;
    private void Awake()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        isShopOpen = false;
        isCheatShopOpen = false;
        isCheat = false;
        sumCoin = 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F1)) isCheatShopOpen = true;
        else if (Input.GetKey(KeyCode.F2)) isCheat = true;
        else if (Input.GetKey(KeyCode.F3)) RetryButton();
        else if (Input.GetKey(KeyCode.F4))
        {
            if (SceneManager.GetActiveScene().buildIndex >= 2)  SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
            else SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
        else if (Input.GetKey(KeyCode.F5)) InGameManager.isGameStart = !InGameManager.isGameStart;


        cheatShop.SetActive(isCheatShopOpen);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WheelType(int num)
    {
        wheelType = num;
        isCheatShopOpen = false;
    }

    public void EngineType(int num)
    {
        engineType = num;
        isCheatShopOpen = false;
    }
}
