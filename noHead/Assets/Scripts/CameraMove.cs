using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera startCamera;
    void Start()
    {
        startCamera=GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startCamera.transform.rotation.y <= 240)
        {
            startCamera.transform.Rotate(0f, 5f * Time.deltaTime, 0f);
        }
        else if (startCamera.transform.rotation.y >= 300)
        {
            startCamera.transform.Rotate(0f, -5f * Time.deltaTime, 0f);
        }

    }
}
