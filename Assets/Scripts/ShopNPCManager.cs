using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPCManager : MonoBehaviour
{
    [SerializeField] private GameObject _npc;
    [SerializeField] private Vector3 _house;
    private Animator _anim;
    private Rigidbody _playerRB;
    private void Start()
    {
        _anim = _npc.GetComponent<Animator>();
        _playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.isShopOpen) _npc.transform.eulerAngles = new Vector3(0, 180, 0);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            StopAllCoroutines();
            StartCoroutine(OnCarMove(other.gameObject));
            _playerRB.velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerCollider")) GameManager.isShopOpen = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            StopAllCoroutines();
            StartCoroutine(OnBackMoveDown());
        }
    }

    IEnumerator OnCarMove(GameObject _gm)
    {
        while (Vector3.Distance(_npc.transform.position, _gm.transform.position) > 4f)
        {
            _npc.transform.LookAt(_npc.transform.position);

            Vector3 _direction = (_gm.transform.position - _npc.transform.position).normalized;

            _npc.transform.position += _direction * Time.deltaTime * 10;

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
