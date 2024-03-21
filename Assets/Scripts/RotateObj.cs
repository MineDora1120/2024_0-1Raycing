using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    [SerializeField] private GameObject _gm;
    void Update()
    {
        _gm.transform.Rotate(Vector3.up * Time.deltaTime);      
    }
}
