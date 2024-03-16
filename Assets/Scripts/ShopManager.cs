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
    [SerializeField] private TextMeshProUGUI title, subtitle, priceTitle, userPrice, userEngine, userWheel;

    private int _itemIndex, arrayIndex;
    private RectTransform _wheelButtonTransform, _engineButtonTransform;

    private string[]
    wheelNamed = new string[] { "�縷 ���� ����", "��� ���� ����", "���� ���� ����" },
    wheelNotice = new string[] { "����� �޸��� ������ �����Դϴ�", "�縷�� �޸��� ������ �����Դϴ�.", "������ �޸��� ������ �����Դϴ�." },
    engineNamed = new string[] { "6���� ����", "8���� ����" },
    engineNotice = new string[] { "6���� �������� �ӵ��� ���ϴ�.", "8���� �������� �ӵ��� ���� ���ϴ�." };

    private int[] wheelItemPrices = { 1250000, 5350000, 8500000 },
    engineItemPrices = { 13500000, 20000000 };

    private TextMeshProUGUI _buyButtonText;
    // Start is called before the first frame update
    void Start()
    {
        shopObj.SetActive(GameManager.isShopOpen);
        _buyButtonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
        _wheelButtonTransform = wheelButton.GetComponent<RectTransform>();
        _engineButtonTransform = engineButton.GetComponentInChildren<RectTransform>();

        ArrayReset();
        StartCoroutine(ChangeText());

        arrayIndex = 0;
    }

    private void Update()
    {
        if (GameManager.isShopOpen) shopObj.SetActive(true);
        else shopObj.SetActive(false);

        userPrice.text = "<sprite=4>" + GameManager.coin;
        userEngine.text = GameManager.engineType.ToString() + "���� ���� �����";
        userWheel.text = (GameManager.wheelType == 0) ? "�⺻ ���� �����" : wheelNamed[GameManager.wheelType-1] + " �����";

        UpdateButtonPos();
        ArrrowLimit();
    }

    IEnumerator ChangeText()
    {
        while (true)
        {
            title.text = (arrayIndex == 0) ? wheelNamed[_itemIndex] : engineNamed[_itemIndex];
            subtitle.text = (arrayIndex == 0) ? wheelNotice[_itemIndex] : engineNotice[_itemIndex];
            priceTitle.text = "<sprite=8>" + ((arrayIndex == 0) ? wheelItemPrices[_itemIndex] : engineItemPrices[_itemIndex]);

            imageShow.sprite = (arrayIndex == 0) ? wheelSpr[_itemIndex] : engineSpr[_itemIndex];

            yield return null;
        }
    }

    IEnumerator NoMoneyPrices()
    {
        StopCoroutine(ChangeText());
        priceTitle.text = "���� �����մϴ�.";
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ChangeText());
    }

    void ArrrowLimit()
    {
        if (_itemIndex == 0) leftArrow.gameObject.SetActive(false);
        else leftArrow.gameObject.SetActive(true);

        if (_itemIndex >= ((arrayIndex == 0) ? 3 : 2) - 1) rightArrow.gameObject.SetActive(false);
        else rightArrow.gameObject.SetActive(true);
    }

    void UpdateButtonPos()
    {
        if (arrayIndex == 0) _wheelButtonTransform.anchoredPosition = new Vector3(-60, 388, 0);
        else _wheelButtonTransform.anchoredPosition = new Vector3(-97, 388, 0);

        if(arrayIndex == 1) _engineButtonTransform.anchoredPosition = new Vector3(-60, 247, 0);
        else _engineButtonTransform.anchoredPosition = new Vector3(-97, 247, 0);
    }

    void ArrayReset()
    {
        _itemIndex = 0;
    }

    public void ClickArrowButton(bool type)
    {
        //type�� true�� ������, false�� ����
        if (type) _itemIndex++;
        else _itemIndex--;
    }

    public void ClickTypeButton(int type)
    {
        //type�� 0�̸� ����, 1�� ����
        arrayIndex = type;
        ArrayReset();
    }

    // Update is called once per frame
}
