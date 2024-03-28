using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMinimapCam : MonoBehaviour
{
    private GameObject _player, _enemy;
    private Quaternion _rotate;
    [SerializeField] private float Ypos = 110;
    [SerializeField] private GameObject _enemySphere, _playerSphere;
    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _enemy = GameObject.FindWithTag("Enemy");
    }
    // Update is called once per frame
    void Update()
    {
        //transform.localEulerAngles = new Vector3(90, 0, 270);
        //transform.position = new Vector3(transform.position.x, Ypos, _player.transform.position.z);

        _enemySphere.transform.SetPositionAndRotation(_enemy.transform.position + new Vector3(0, 20, 0), Quaternion.identity);
        _playerSphere.transform.SetPositionAndRotation(_player.transform.position + new Vector3(0, 20, 0), Quaternion.identity);
    }
}
