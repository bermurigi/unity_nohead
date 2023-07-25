using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Linq.Expressions;

public class GhostMotionController : MonoBehaviourPunCallbacks, IPunObservable //기존스크립트에서 건드린부분 MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    float _speed = 10.0f;
    [SerializeField]
    float _turnSpeed = 4.0f;

    [SerializeField]
    GameObject MLight;
    private bool isOn = false;
    
    //오디오
    AudioSource audioSource;
    public bool soundOnOff = false;

    [SerializeField]
    Animator GAnimator;
    
    //멀티플레이어 관련 변수선언
    public Text NickNameText; 
    public PhotonView PV;
    public Camera camera;
    //캐릭터 색 설정
    public Material[] mat = new Material[3];
    public SkinnedMeshRenderer playerRenderer;
    
   

    public int i = 0;
    //0=노랑
    //1=검정
    //2=빨강
    //캐릭터 우산설정도만들어야함
        

    void Awake() 
    {
        //멀티플레이어 입장시 기본세팅
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.green : Color.red;
        this.gameObject.tag = "Player"; //적 오브젝트가 추적하기 위해서 태그 추가 여기 입니다!!@@!@!!@@!@!@
        camera.enabled = false;
        MLight.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        
        
       
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
    
    [PunRPC]
    private void RPC_ChangeMaterial(int materialIndex)
    {
        i = materialIndex;   //재지정
        // RPC를 수신한 모든 클라이언트의 플레이어 메테리얼을 변경
        if (materialIndex >= 0 && materialIndex < mat.Length)
        {
            playerRenderer.material = mat[materialIndex];
            
        }
    }
    

    void Update()
    {
        photonView.RPC("RPC_ChangeMaterial", RpcTarget.AllBuffered, i);
        audioSource.mute = true;
        soundOnOff = false;
        
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

            //light On/Off
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                
                isOn = !isOn; // 손전등의 상태를 변경합니다.
                photonView.RPC("ToggleFlashlight", RpcTarget.All, isOn);
                
            }

            if (Input.GetKey(KeyCode.Space))
            {
                audioSource.mute = false;
                soundOnOff = true;

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
    [PunRPC]
    private void ToggleFlashlight(bool state)
    {
        // 손전등의 상태에 따라 효과를 적용하거나 끄는 등의 동작을 수행합니다.
        if (state)
        {
            MLight.SetActive(true);
        }
        else
        {
            MLight.SetActive(false);
        }
    }

    
    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)  
    {
        
    }
}
