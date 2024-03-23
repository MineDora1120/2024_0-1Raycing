using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject _status, _rank, _playMenu, mainUi;
    private TextMeshProUGUI _rankText;

    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _spr;

    private bool isActiveWindow = false;
    private int selectindex = 0;
    private string[] _str = new string[3] { "사막지형", "산악지형", "도시지역" };
    // Start is called before the first frame update
    void Start()
    {
        selectindex = 0;
        isActiveWindow = true;
        _rankText = _rank.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _status.SetActive(isActiveWindow);
        mainUi.SetActive(!GameManager.isShopOpen);
    }

    void RankText()
    {
        string _tmpStr = "<size=65><color=#3CB371>랭크 목록\r\n";

        for (int i = 0; i < 3; i++)
        {
            _tmpStr += "<size=45><color=#4169E1>" + _str[i] + "\r\n";
            for (int j = 0; j < 5; j++)
            {
                _tmpStr += "<size=30><color=black>" + (j + 1) + ". " + ((JSONManager._rankData[i][j].score == 1) ? "등록되지 않음" : "시간: " + JSONManager._rankData[i][j].time + " ,점수 : " + JSONManager._rankData[i][j].score);
                _tmpStr += (j == 2) ? "\n" : " ";
            }
            _tmpStr += "\r\n";
        }
        _rankText.text = _tmpStr;
    }

    public void OnMenuButtonClick(int type)
    {

        switch (type)
        {
            case 0:
                _rank.SetActive(true);
                RankText();
                isActiveWindow = false;
                break;
            case 1:
                GameManager.isShopOpen = true;
                break;
            case 2:
                _playMenu.SetActive(true);
                isActiveWindow = false;
                break;
        }
    }

    public void CancelButton()
    {
        isActiveWindow = true;
        _rank.SetActive(false);
    }


    public void ArrowClick(bool eekta)
    {
        if (eekta) selectindex++;
        else selectindex--;
    }
}
