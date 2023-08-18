using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float _turnSpeed = 4.0f;
    private float xRotate = 0.0f;

    private void Start()
    {
        transform.eulerAngles = new Vector3(xRotate, transform.eulerAngles.y, 0);
    }
    void Update()
    {
        MouseRotation();

    }

    void MouseRotation()
    {

        float xRotateSize = -Input.GetAxis("Mouse Y") * _turnSpeed;
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        transform.eulerAngles = new Vector3(xRotate, transform.eulerAngles.y, 0);
    }
}
