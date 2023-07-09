using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMotionController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;
    [SerializeField]
    float _turnSpeed = 4.0f;

    [SerializeField]
    Animator GAnimator;
    

    void Start()
    {
        Managers.Input.KeyAction -= Onkeyboard; 
        Managers.Input.KeyAction += Onkeyboard;
        
    }

    void Update()
    {
        GAnimator.SetBool("Fly", false);
        MouseRotation();

    }
    void MouseRotation()
    {
        // 좌우로 움직인 마우스의 이동량 * 속도에 따라 카메라가 좌우로 회전할 양 계산
        float yRotateSize = Input.GetAxis("Mouse X") * _turnSpeed;
        // 현재 y축 회전값에 더한 새로운 회전각도 계산
        float yRotate = transform.eulerAngles.y + yRotateSize;


        // 카메라 회전량을 카메라에 반영(X, Y축만 회전)
        transform.eulerAngles = new Vector3(0, yRotate, 0);
    }

    void Onkeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            GAnimator.SetBool("Fly", true);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += transform.TransformDirection (Vector3.forward * Time.deltaTime * _speed);                       

        }
        if (Input.GetKey(KeyCode.S))
        {
            GAnimator.SetBool("Fly", true);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            GAnimator.SetBool("Fly", true);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            GAnimator.SetBool("Fly", true);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed);
        }
    }
}
