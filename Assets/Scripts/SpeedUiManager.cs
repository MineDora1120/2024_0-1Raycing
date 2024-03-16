using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SpeedUiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _speedText;
    [SerializeField] Image _speedImage;

    private Rigidbody _rb;
    // Start is called before the first frame update

    private void Start()
    {
        _rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _speedText.text = ((int) (_rb.velocity.magnitude * 2.4f)).ToString();
        _speedImage.fillAmount = (_rb.velocity.magnitude * 2.4f) / 100;
    }
}
