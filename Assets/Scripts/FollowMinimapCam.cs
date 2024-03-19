using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMinimapCam : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(90, 0, 270);
    }
}
