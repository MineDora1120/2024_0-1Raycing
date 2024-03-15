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
    wheelNamed = new string[] { "��� ���� ����", "�縷 ���� ����", "���� ���� ����" },
    wheelNotice = new string[] { "����� �޸��� ������ �����Դϴ�", "�縷�� �޸��� ������ �����Դϴ�.", "������ �޸��� ������ �����Դϴ�." },
    engineNamed = new string[] { "6���� ����", "8���� ����" },
    engineNotice = new string[] { "6���� �������� �ӵ��� ���ϴ�.", "8���� �������� �ӵ��� ���� ���ϴ�." };

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
        //type�� true�� ������, false�� ����
       if(type) 
    }

    // Update is called once per frame
}
