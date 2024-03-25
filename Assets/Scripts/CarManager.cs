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

    public float groundSpeed = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = Vector3.zero;
        groundSpeed = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.isShopOpen) CarBreak(20000);
        if (!InGameManager.isGameStart) _rb.velocity = Vector3.zero;

        AutoBreak();
        RotateWheels();
        WheelObjRotate();
        if (GameManager.sceneIndex == GameManager.wheelType+1) _rb.AddForce(transform.TransformDirection(Vector3.forward) * GameManager.engineType * Input.GetAxis("Vertical") * 2, ForceMode.Impulse);
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
        if (!InGameManager.isGameStart) return;

        for(int i = 0; i < 4; i++)
        {
            if (i < 2) wheelColliders[i].steerAngle =  wheelDir * Input.GetAxis("Horizontal");
            else wheelColliders[i].motorTorque = GameManager.engineType * speed * Input.GetAxis("Vertical") *  ((GameManager.sceneIndex != GameManager.wheelType + 1) ?  groundSpeed : 1);
        }
    }

    public void CarBreak(float value)
    {
        foreach (WheelCollider _wheel in wheelColliders)
        {
            _wheel.brakeTorque = value;
        }
    }

    void AutoBreak()
    {
        if (Input.GetAxis("Vertical") != _tmpWheelDir) BreakSpecialValue(1);
        else if (Input.GetAxis("Vertical") == _tmpWheelDir) CarBreak(0);
    }

    public void BreakSpecialValue(float maxValue)
    {
        if (_rb.velocity.magnitude > maxValue) CarBreak(speed * 20000);
        else _tmpWheelDir = Input.GetAxis("Vertical");
    }
}
