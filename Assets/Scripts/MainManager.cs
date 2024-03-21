using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject _status, _rank;
    private TextMeshProUGUI _rankText;

    private bool isActiveWindow = false;
    // Start is called before the first frame update
    void Start()
    {
        isActiveWindow = false;
    }

    // Update is called once per frame
    void Update()
    {
        _status.SetActive(!isActiveWindow);
    }

    void RankText()
    {

    }

    public void OnMenuButtonClick(int type)
    {
        switch (type)
        {
            case 0:
                _rank.SetActive(true);
                break;
        }
    }
}
