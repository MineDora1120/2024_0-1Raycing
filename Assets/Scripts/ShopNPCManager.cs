using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPCManager : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(OnCarMove(other.gameObject));
        }
    }

    IEnumerator OnCarMove(GameObject _gm)
    {
        while (true)
        {
            transform.LookAt(transform.position);
            Vector3 _vec = (transform.position - _gm.transform.position).normalized;

            transform.position += _vec * speed;

            yield return null;
        }
    }
}
