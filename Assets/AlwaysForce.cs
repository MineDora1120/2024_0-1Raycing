using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysForce : MonoBehaviour
{
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_rb.velocity.magnitude < 500) _rb.AddForce(Vector3.right, ForceMode.Impulse);
    }
}
