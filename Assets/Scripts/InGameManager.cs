using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText, _timeText;

    public static bool isGameStart = false;
    private int count = 5;

    private Vector2 _vec;
    private float _scaleSpeed, deltaTime, _oneBornTime;

    // Start is called before the first frame update
    void Start()
    {
        deltaTime = 0;
        _oneBornTime = 120;

        count = 5;
        _scaleSpeed = 50f;
        isGameStart = false;
        StartCoroutine(GameStart());
    }

    private void Update()
    {
        _countText.transform.localScale = Vector2.Lerp(_vec, _countText.transform.localScale, Time.deltaTime * _scaleSpeed);
        _timeText.text = (int)(_oneBornTime / 60) + ":" + (int)(_oneBornTime % 60);
    }

    private void FixedUpdate()
    {
        if (isGameStart && !GameManager.isShopOpen)
        {
            deltaTime += Time.deltaTime;
            _oneBornTime -= Time.deltaTime;

            if(_oneBornTime <= 0) GameOverType("Ai");
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
            yield return new WaitForSeconds(0.5f);
            _vec = new Vector3(0, 0);
            yield return new WaitForSeconds(0.5f);

            _countText.transform.localScale = Vector3.zero;
            _countText.color = Random.ColorHSV();
        }

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
        if (type == "Player")
        {
            _countText.text = "Win!";
            _countText.color = Color.green;
            _vec = new Vector3(1, 1);

            GameManager.coin += GameManager.sumCoin;
            GameManager.sumCoin = 0;
        }
        else
        {
            _countText.text = "Fail!";
            _countText.color = Color.yellow;
            _vec = new Vector3(1, 1);
        }
        isGameStart = false;
    }
}
