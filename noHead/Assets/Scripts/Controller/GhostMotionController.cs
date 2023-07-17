using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GhostMotionController : MonoBehaviourPunCallbacks, IPunObservable //기존스크립트에서 건드린부분 MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    float _speed = 10.0f;
    [SerializeField]
    float _turnSpeed = 4.0f;

    [SerializeField]
    Animator GAnimator;
    
    //멀티플레이어 관련 변수선언
    public Text NickNameText; 
    public PhotonView PV;
    public Camera camera;

    void Awake() 
    {
        //멀티플레이어 입장시 기본세팅
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.green : Color.red;
        this.gameObject.tag = "Player"; //적 오브젝트가 추적하기 위해서 태그 추가
        camera.enabled = false;
        if (PV.IsMine)
        {
            camera.enabled = true;

            this.gameObject.layer = 7;
           
            ChangeLayer(transform);

        }
    }
    

    void Start()
    {
        Managers.Input.KeyAction -= Onkeyboard; 
        Managers.Input.KeyAction += Onkeyboard;
        
        
    }

    void Update()
    {
        
        if (PV.IsMine) //본인 캐릭터만 조종할 수 있게해줌 (바라보는방향)
        {
            GAnimator.SetBool("Fly", false);
            MouseRotation();
        }

    }
    void MouseRotation()
    {
        // �¿�� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� �¿�� ȸ���� �� ���
        float yRotateSize = Input.GetAxis("Mouse X") * _turnSpeed;
        // ���� y�� ȸ������ ���� ���ο� ȸ������ ���
        float yRotate = transform.eulerAngles.y + yRotateSize;


        // ī�޶� ȸ������ ī�޶� �ݿ�(X, Y�ุ ȸ��)
        transform.eulerAngles = new Vector3(0, yRotate, 0);
    }

    void Onkeyboard()
    {
        if (PV.IsMine) //본인 캐릭터만 조종할 수 있게해줌
        {


            if (Input.GetKey(KeyCode.W))
            {
                GAnimator.SetBool("Fly", true);
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
                transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);

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
    //기존스크립트에서 추가된 내용 ↓(윤기)
    
    void ChangeLayer(Transform parent) //자식들 레이어바꿔주기 카메라 culling
    {
        // 부모 오브젝트의 자식들에 대해 레이어를 변경합니다.
        foreach (Transform child in parent)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Player");

            // 자식 오브젝트가 다른 자식을 가지고 있다면 재귀적으로 호출합니다.
            if (child.childCount > 0)
            {
                ChangeLayer(child);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)  
    {
        
    }
}
