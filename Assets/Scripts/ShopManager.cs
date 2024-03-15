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

    private int _itemIndex, arrayIndex, wheelMaxIndex = 2, engineMaxIndex = 1;

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
        ArrayReset();
    }

    private void Update()
    {
        title.text = (arrayIndex == 0) ? wheelNamed[_itemIndex] : engineNamed[_itemIndex];
        subtitle.text = (arrayIndex == 0) ? wheelNotice[_itemIndex] : engineNotice[_itemIndex];
    }

    void ArrayReset()
    {
        _itemIndex = 0;
        arrayIndex = 0;
    }

    public void ClickArrowButton(bool type)
    {
        //type�� true�� ������, false�� ����
        if (type) _itemIndex++;
        else _itemIndex--;
    }

    // Update is called once per frame
}
