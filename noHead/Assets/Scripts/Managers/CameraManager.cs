using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera startcamera;

    public void covertCamera()
    {
        startcamera.enabled = false;
    }
}
