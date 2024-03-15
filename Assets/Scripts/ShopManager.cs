using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject shopObj;
    [SerializeField] private Button leftArrow, rightArrow, buyButton, wheelButton, engineButton;
    [SerializeField] private Image imageShow;
    [SerializeField] private Sprite[] wheelSpr, engineSpr;
    [SerializeField] private TextMeshPro title, subtitle, priceTitle;

    private int _wheelIndex, _engineIndex, arrayIndex;

    private string[]
    wheelNamed = new string[] { "산악 전용 바퀴", "사막 전용 바퀴", "도심 전용 바퀴" },
    wheelNotice = new string[] { "산악을 달리기 적합한 바퀴입니다", "사막을 달리기 적합한 바퀴입니다.", "도심을 달리기 적합한 바퀴입니다." },
    engineNamed = new string[] { "6기통 엔진", "8기통 엔진" },
    engineNotice = new string[] { "6개의 기통으로 속도를 냅니다.", "8개의 기통으로 속도를 더욱 냅니다." };

    private TextMeshPro _buyButtonText;
    // Start is called before the first frame update
    void Start()
    {
        shopObj.SetActive(GameManager.isShopOpen);
        _buyButtonText = buyButton.GetComponentInChildren<TextMeshPro>();
    }

    void 

    public void ClickArrowButton(bool type)
    {
        //type이 true면 오른쪽, false면 왼쪽
       if(type) 
    }

    // Update is called once per frame
}
