using System.Collections;
using System.Collections.Generic;           
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField] private GameObject[] wheelObjects;
    [SerializeField] private WheelCollider[] wheelColliders;
    [SerializeField] private float speed = 1000, wheelDir = 40;

    private Vector3 _transform;
    private Quaternion _rotation;
    private float _tmpWheelDir;
    private Rigidbody _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AutoBreak();
        RotateWheels();
        WheelObjRotate();
    }

    void WheelObjRotate()
    {
        for (int i = 0; i < 4; i++)
        {
            wheelColliders[i].GetWorldPose(out _transform, out _rotation);
            wheelObjects[i].transform.SetPositionAndRotation(_transform, _rotation);
        }
    }

    void RotateWheels()
    {
        for(int i = 0; i < 4; i++)
        {
            if (i < 2) wheelColliders[i].steerAngle = wheelDir * Input.GetAxis("Horizontal");
            else wheelColliders[i].motorTorque = speed * Input.GetAxis("Vertical");
        }
    }

    void CarBreak(float value)
    {
        foreach (WheelCollider _wheel in wheelColliders)
        {
            _wheel.brakeTorque = value;
        }
    }

    void AutoBreak()
    {
        if (Input.GetAxis("Vertical") != _tmpWheelDir)
        {

            if (_rb.velocity.magnitude > 5) CarBreak(speed * 20000);
            else _tmpWheelDir = Input.GetAxis("Vertical");

        }
        else if (Input.GetAxis("Vertical") == _tmpWheelDir) CarBreak(0);
    }
}
