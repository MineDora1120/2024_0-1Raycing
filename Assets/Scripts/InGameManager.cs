using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText, _timeText;
    [SerializeField] private Scrollbar _percentBar;
    [SerializeField] private GameObject retryMenu, retryMenuButton, retryMainUI;
    [SerializeField] private Image _retryButtonChange;
    [SerializeField] private Sprite[] _retryButtonSpr; 

    private int[] sceneCount = { 120, 180, 210 };
    public static bool isGameStart = false;
    private int count = 5;

    private RectTransform _retryRect, _retryButtonRect;
    private Vector2 _vec;
    private float _scaleSpeed, deltaTime, _oneBornTime;
    private bool win = false;
    GameSceneManager _sceneMananger;

    // Start is called before the first frame update
    void Start()
    {
        _sceneMananger = GameObject.FindWithTag("SceneManager").GetComponent<GameSceneManager>();
        deltaTime = 0;
        _oneBornTime = sceneCount[GameManager.sceneIndex - 1];

        win = false;
        count = 5;
        _scaleSpeed = 50f;
        isGameStart = false;

        if (retryMenu != null)
        {
            retryMainUI.SetActive(false);
            _retryRect = retryMenu.GetComponent<RectTransform>();
            _retryButtonRect = retryMenuButton.GetComponent<RectTransform>();
        }
        StartCoroutine(GameStart());
    }

    IEnumerator Text()
    {
        for (int i = 0; i < 150; i++)
        {
            _countText.transform.localScale = Vector2.Lerp(_vec, _countText.transform.localScale, Time.deltaTime * _scaleSpeed);
            _timeText.text = (int)(_oneBornTime / 60) + ":" + (int)(_oneBornTime % 60);
            _percentBar.size = _oneBornTime / sceneCount[GameManager.sceneIndex - 1];
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (isGameStart)
        {
            deltaTime += Time.deltaTime;
            _oneBornTime -= Time.deltaTime;

            if (_oneBornTime <= 0) GameOverType("Ai");
        }

        if(!isGameStart && GameManager.isShopOpen) retryMenuButton.SetActive(false);
        else retryMenuButton.SetActive(true);
    }

    IEnumerator ChangeUiText()
    {
        while(true)
        {
            _timeText.text = (int)(_oneBornTime / 60) + ":" + (int)(_oneBornTime % 60);
            _percentBar.size = _oneBornTime / sceneCount[GameManager.sceneIndex - 1];
            yield return new WaitForSeconds(1);
        }
    }

    // Update is called once per frame
    IEnumerator GameStart()
    {
        _scaleSpeed = 50;
        for (count = 5; count >= 0; count--)
        {
            _vec = new Vector3(1, 1);
            _countText.text = (count != 0) ? count.ToString() : "Go!";
            StartCoroutine(Text());
            yield return new WaitForSeconds(0.5f);
            _vec = new Vector3(0, 0);
            StartCoroutine(Text());
            yield return new WaitForSeconds(0.5f);

            _countText.transform.localScale = Vector3.zero;
            _countText.color = Random.ColorHSV();
        }
        StartCoroutine(ChangeUiText());
        _scaleSpeed = 100;
        isGameStart = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGameStart) return;
        _scaleSpeed = 50;
        if (other.gameObject.CompareTag("PlayerCollider")) GameOverType("Player");
        else GameOverType("Ai");
    }


    void GameOverType(string type)
    {
        StopCoroutine(ChangeUiText());
        retryMainUI.SetActive(true);

        if (type == "Player")
        {
            _countText.text = "Win!";
            _countText.color = Color.green;
            _vec = new Vector3(1, 1);

            win = true;
            StartCoroutine(Text());

            GameManager.coin += GameManager.sumCoin;
            GameManager.sumCoin = 0;
            GameManager.clear[GameManager.sceneIndex - 1] = 1;

            StartCoroutine(EndMenu());
            SumRank();
        }
        else
        {
            _countText.text = "Fail!";
            _countText.color = Color.yellow;
            _vec = new Vector3(1, 1);
            StartCoroutine(Text());
            StartCoroutine(EndMenu());

            _scaleSpeed = 50;
        }
        isGameStart = false;
    }

    IEnumerator EndMenu()
    {
        if (win) _retryButtonChange.sprite = _retryButtonSpr[1];
        else _retryButtonChange.sprite = _retryButtonSpr[0];

        _retryRect.anchoredPosition = new Vector3(1920, 0);
        _retryButtonRect.anchoredPosition = new Vector3(0, -500);
        for (int i = 0; i <150; i++)
        {
            _retryRect.anchoredPosition = Vector3.Lerp(_retryRect.anchoredPosition, Vector3.zero, Time.deltaTime * 10);
            _retryButtonRect.anchoredPosition = Vector3.Lerp(_retryButtonRect.anchoredPosition, Vector3.zero, Time.deltaTime * 10);
            yield return null;
        }
    }

    void SumRank()
    {
        int i, index;
        float score, time;

        score = (_oneBornTime / 120) * 100;
        time = deltaTime;
        index = GameManager.sceneIndex - 1;

        for (i = 4; i > 0; i--)
        {
            if (JSONManager._rankData[index][i].score >= score) break;
        }

        PlayerData _tmpDB = new PlayerData();

        _tmpDB.time = (int)time;
        _tmpDB.score = (int)score;

        JSONManager._rankData[index][i] = _tmpDB;

        JSONManager.SaveRank();
    }

    public void OnClickButtonType(int num)
    {
        switch(num)
        {
            case 0:
                GameManager.isShopOpen = true;
                break;
            case 1:
                StartCoroutine(_sceneMananger.EndScene());
                StartCoroutine(SceneLoad(0));
                break;
            case 2:
                if (win) StartCoroutine(SceneLoad(GameManager.sceneIndex+1));
                else StartCoroutine(SceneLoad(GameManager.sceneIndex));
                break;
        }
    }

    IEnumerator SceneLoad(int index)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }
}
