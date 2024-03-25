using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCrash : MonoBehaviour
{
    [SerializeField] private CarManager _carManager;

    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _carManager.CarBreak(1000000);
            _carManager.groundSpeed = 0.5f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _carManager.groundSpeed = 1f;
            _carManager.CarBreak(0);
        }
    }
}
