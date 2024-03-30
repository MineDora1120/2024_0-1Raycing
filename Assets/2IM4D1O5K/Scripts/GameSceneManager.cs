using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartScene());
    }
    public IEnumerator EndScene()
    {
        gameObject.SetActive(true);
        Image _image = GetComponent<Image>();
        Color _tmpColor = Color.white;
        for(float i = 0; i < 1; i+=Time.deltaTime)
        {
            _image.color = _tmpColor;

            _tmpColor.a = i;
            yield return null;
        }
    }
    public IEnumerator StartScene()
    {
        Image _image = GetComponent<Image>();
        Color _tmpColor = Color.white;
        for (float i = 1; i > 0; i -= Time.deltaTime)
        {
            _image.color = _tmpColor;

            _tmpColor.a = i;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
