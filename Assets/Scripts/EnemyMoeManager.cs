using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoeManager : MonoBehaviour
{
    [SerializeField] private Vector3 _pos;
    [SerializeField] private Rigidbody yourRb;
    [SerializeField] private GameObject[] _wheelObj;

    private GameObject _player;
    private NavMeshAgent _nav;
    private Rigidbody _playerRb;


    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerRb = _player.GetComponent<Rigidbody>();
        _nav = GetComponent<NavMeshAgent>();

        yourRb.centerOfMass = Vector3.zero;
        _nav.destination = _pos;
    }

    private void FixedUpdate()
    {
        if (GameManager.isShopOpen || !InGameManager.isGameStart) _nav.speed = 0;
        else _nav.speed = _playerRb.velocity.magnitude * Random.Range(0.5f, 1.2f) + Random.Range(1, 2);
       

        foreach(GameObject wheel in _wheelObj)
        {
            wheel.transform.Rotate(Vector3.right);
        }
    }
}
