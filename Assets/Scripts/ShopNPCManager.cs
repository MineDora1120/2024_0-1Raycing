using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPCManager : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject _npc;
    [SerializeField] private Vector3 _house;
    private Animator _anim;

    private void Start()
    {
        _anim = _npc.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.isShopOpen) _npc.transform.eulerAngles = new Vector3(0, 180, 0);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(OnCarMove(other.gameObject));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) GameManager.isShopOpen = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(OnBackMoveDown());
        }
    }

    IEnumerator OnCarMove(GameObject _gm)
    {
        while (Vector3.Distance(_npc.transform.position, _gm.transform.position) > 1.5f)
        {
            _npc.transform.LookAt(_npc.transform.position);

            Vector3 _direction = (_gm.transform.position - _npc.transform.position).normalized;

            _npc.transform.position += _direction * Time.deltaTime;

            _anim.SetBool("IsRun", true);

            yield return null;
        }
        _anim.SetBool("IsRun", false);
    }

    IEnumerator OnBackMoveDown()
    {
        while (Vector3.Distance(_npc.transform.position, _house) > 0.1f)
        {
            _npc.transform.LookAt(_house);

            Vector3 _direction = (_house - _npc.transform.position).normalized;

            _npc.transform.position += _direction * Time.deltaTime;

            _anim.SetBool("IsRun", true);
            GameManager.isShopOpen = false;

            yield return null;
        }
        _anim.SetBool("IsRun", false);
    }
}
