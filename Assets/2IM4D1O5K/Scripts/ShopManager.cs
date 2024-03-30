using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject shopObj;
    [SerializeField] private Button leftArrow, rightArrow, buyButton, wheelButton, engineButton;
    [SerializeField] private Image imageShow;
    [SerializeField] private Sprite[] wheelSpr, engineSpr, buttonList;
    [SerializeField] private TextMeshProUGUI title, subtitle, priceTitle, userPrice, userEngine, userWheel;

    private GameObject _status;
    private int _itemIndex, arrayIndex, _price;
    private RectTransform _wheelButtonTransform, _engineButtonTransform, _statusRect;
    private string _name, _notice;
    private bool changePriceText = false, isGameMenu = false;

    enum ButtonType
    {
        Yello,
        Blue,
        Green
    };

    private string[]
    wheelNamed = new string[] { "사막 전용 바퀴", "산악 전용 바퀴", "도심 전용 바퀴" },
    wheelNotice = new string[] { "산악을 달리기 적합한 바퀴입니다", "사막을 달리기 적합한 바퀴입니다.", "도심을 달리기 적합한 바퀴입니다." },
    engineNamed = new string[] { "6기통 엔진", "8기통 엔진" },
    engineNotice = new string[] { "6개의 기통으로 속도를 냅니다.", "8개의 기통으로 속도를 더욱 냅니다." };

    private int[] wheelItemPrices = { 1250000, 5350000, 8500000 },
    engineItemPrices = { 13500000, 20000000 };

    private TextMeshProUGUI _buyButtonText;
    // Start is called before the first frame update
    void Awake()
    {
        isGameMenu = (SceneManager.GetActiveScene().buildIndex == 0);
        if (isGameMenu)
        {
            _status = GameObject.FindWithTag("Respawn");
            _statusRect = _status.GetComponent<RectTransform>();
        }

        shopObj.SetActive(GameManager.isShopOpen);
        _buyButtonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
        _wheelButtonTransform = wheelButton.GetComponent<RectTransform>();
        _engineButtonTransform = engineButton.GetComponentInChildren<RectTransform>();

        ArrayReset();

        arrayIndex = 0;
        changePriceText = false;
    }

    private void Update()
    {
        shopObj.SetActive(GameManager.isShopOpen);
        if(isGameMenu)
        {
            if (GameManager.isShopOpen) _statusRect.anchoredPosition = new Vector2(225f, -435);
            else _statusRect.anchoredPosition = new Vector2(0, -435);
        }

        _name = (arrayIndex == 0) ? wheelNamed[_itemIndex] : engineNamed[_itemIndex];
        _notice = (arrayIndex == 0) ? wheelNotice[_itemIndex] : engineNotice[_itemIndex];
        _price = ((arrayIndex == 0) ? wheelItemPrices[_itemIndex] : engineItemPrices[_itemIndex]);

        UpdateButtonPos();
        ArrrowLimit();
        SubText();
        ButtonChange();
        ChangeText();
    }

    void ChangeText()
    {
            title.text = _name;
            subtitle.text = _notice;
            if(!changePriceText) priceTitle.text = "<sprite=8>" + _price;

            imageShow.sprite = (arrayIndex == 0) ? wheelSpr[_itemIndex] : engineSpr[_itemIndex];
    }

    void SubText()
    {
        if (!isGameMenu) userPrice.text = "<sprite=4>" + (GameManager.coin + " + <color=red>" + GameManager.sumCoin);
        else userPrice.text = "<sprite=4>" + (GameManager.coin);
        userEngine.text = GameManager.engineType.ToString() + "기통 엔진 사용중";
        userWheel.text = (GameManager.wheelType == 4) ? "기본 바퀴 사용중" : wheelNamed[GameManager.wheelType] + " 사용중";
    }

    void ButtonChange()
    {
        int[] _arr = (arrayIndex == 0) ? GameManager.wheelItemList : GameManager.engineItemList;
        int type = (arrayIndex == 0) ? GameManager.wheelType : GameManager.engineType;

        if (_arr[_itemIndex] == 1)
        {
            if (type == ((arrayIndex == 0) ? _itemIndex : ConvertEngineType(_itemIndex))) BuyButtonTextBoxType("해제", ButtonType.Green);
            else BuyButtonTextBoxType("적용", ButtonType.Blue);
        }

        else BuyButtonTextBoxType("구입", ButtonType.Yello);
    }

    void BuyButtonTextBoxType(string _str, ButtonType type)
    {
        buyButton.image.sprite = buttonList[((int)type)];
        _buyButtonText.text = _str;
    }

    IEnumerator NoMoneyPrices()
    {
        changePriceText = true;
        priceTitle.text = "돈이 부족합니다.";
        yield return new WaitForSeconds(0.5f);
        changePriceText = false;
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

        if (arrayIndex == 1) _engineButtonTransform.anchoredPosition = new Vector3(-60, 247, 0);
        else _engineButtonTransform.anchoredPosition = new Vector3(-97, 247, 0);
    }

    void ArrayReset()
    {
        _itemIndex = 0;
    }

    public void ClickArrowButton(bool type)
    {
        //type이 true면 오른쪽, false면 왼쪽
        if (type) _itemIndex++;
        else _itemIndex--;
    }

    public void ClickBuyButton()
    {
        int[] _arr = (arrayIndex == 0) ? GameManager.wheelItemList : GameManager.engineItemList;

        if (_arr[_itemIndex] == 1)
        {
            if (arrayIndex == 0) GameManager.wheelType = (GameManager.wheelType == _itemIndex) ? 4 : _itemIndex;
            else if (arrayIndex == 1) GameManager.engineType = (GameManager.engineType == ConvertEngineType(_itemIndex)) ? 4 : ConvertEngineType(_itemIndex);
        }

        else if (_price > GameManager.coin && !GameManager.isCheat) StartCoroutine(NoMoneyPrices());

        else
        {
            if(!GameManager.isCheat) GameManager.coin -= _price;

            if (arrayIndex == 0) GameManager.wheelItemList[_itemIndex] = 1;
            else if (arrayIndex == 1) GameManager.engineItemList[_itemIndex] = 1;
        }
    }

    public void CancelButton()
    {
        GameManager.isShopOpen = false;
    }
    int ConvertEngineType(int _index)
    {
        return (_index * 2) + 6;
    }

    public void ClickTypeButton(int type)
    {
        //type이 0이면 바퀴, 1면 엔진
        arrayIndex = type;
        ArrayReset();
    }

    // Update is called once per frame
}
