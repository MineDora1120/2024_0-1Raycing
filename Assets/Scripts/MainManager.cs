using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject _status, _rank, _playMenu, mainUi;
    [SerializeField] private Button _leftArr, _rightArr, _startArr;
    [SerializeField] private Image _startMenuImage;
    [SerializeField] private Sprite[] _spr;

    private GameSceneManager _localSceneManager;
    private bool isActiveWindow = false;
    private TextMeshProUGUI _rankText, _mapTitle;
    private int selectindex = 0;
    private string[] _str = new string[3] { "사막지형", "산악지형", "도시지역" };
    private string[] _mapNameStr = new string[3] { "Desert", "Mountain", "City" };
    // Start is called before the first frame update
    void Start()
    {
        CancelButton();

        _localSceneManager = GameObject.FindWithTag("SceneManager").GetComponent<GameSceneManager>();
        selectindex = 0;
        isActiveWindow = true;
        _rankText = _rank.GetComponentInChildren<TextMeshProUGUI>();
        _mapTitle = _playMenu.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _status.SetActive(isActiveWindow);
        mainUi.SetActive(!GameManager.isShopOpen);

        if (_playMenu.activeSelf) StartMenuUpdate();
    }

    void StartMenuUpdate()
    {
        _mapTitle.text = "맵 선택\r\n<size=40><color=#0030FF>" + _str[selectindex];

        if (selectindex == 0) _leftArr.gameObject.SetActive(false);
        else _leftArr.gameObject.SetActive(true);

        if (selectindex == 2) _rightArr.gameObject.SetActive(false);
        else _rightArr.gameObject.SetActive(true);

        _startMenuImage.sprite = _spr[selectindex];
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
    public void GameStartButton()
    {
        StartCoroutine(_localSceneManager.EndScene());
        Invoke("PlayScene", 1f);
    }

    public void CancelButton()
    {
        isActiveWindow = true;
        _rank.SetActive(false);
        _playMenu.SetActive(false);
    }

    public void PlayScene()
    {
        SceneManager.LoadScene(_mapNameStr[selectindex]);
    }


    public void ArrowClick(bool eekta)
    {
        if (eekta) selectindex++;
        else selectindex--;
    }
}
