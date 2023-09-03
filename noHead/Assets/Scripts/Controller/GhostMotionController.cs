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
    public bool FollowingOnOff = false;

    [SerializeField]
    Animator GAnimator;
    
    //멀티플레이어 관련 변수선언
    public Text NickNameText; 
    public PhotonView PV;
    public Camera camera;
    //캐릭터 색 설정
    public Material[] mat = new Material[3];
    public SkinnedMeshRenderer playerRenderer;
    public bool test;
    public GameObject keyCountText;
   

    public int i = 0;
    //0=노랑
    //1=검정
    //2=빨강
    //캐릭터 우산설정도만들어야함
    
    public RaycastHit rayHit;
    private Ray ray;
    private float MAX_DISTANCE = 2.0f;
    private Transform highlight;
    private Transform selection;
    private int layerMask;
    public Pick pickUpscript;
    private GameObject nowObject_clone;
    private StartManager startManger;
    private GameObject startMangerobject;

    public AudioSource FlashLightSound;
    
    
    //숨기 & 엔딩
    private GameObject hidingC;
    private Animator Hanimator;
    private bool openorclose;
    [SerializeField]private bool inputproceed;
    private GameObject navmeshlink;
    private float delay;
    
    
    
    
        

    void Awake() 
    {
        pickUpscript = GetComponent<Pick>();
        //멀티플레이어 입장시 기본세팅
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.green : Color.red;
        this.gameObject.tag = "Player"; //적 오브젝트가 추적하기 위해서 태그 추가 여기 입니다!!@@!@!!@@!@!@
        camera.enabled = false;
        keyCountText.SetActive(false);
        MLight.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        startMangerobject = GameObject.Find("StartManager");
        startManger = startMangerobject.GetComponent<StartManager>();
        
        
        if (MaterialManager.Instance != null)
        {
            MaterialManager.Instance.MyNum = PV.Owner.ActorNumber;
        }
        Debug.Log(PV.Owner.ActorNumber);
        audioSource.mute = true;
        
       
        if (PV.IsMine)
        {
            camera.enabled = true;
            keyCountText.SetActive(true);

            this.gameObject.layer = 7;
            ChangeLayer(transform);
            
            
            

        }
        
        //숨기 & 엔딩
        hidingC = GameObject.Find("HidingCurtain");
        Hanimator = hidingC.GetComponent<Animator>();
        navmeshlink = GameObject.Find("BathLink3");
        openorclose = true;
        delay = 1.8f;



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


        inputproceed = false;
        
        if (PV.IsMine) //본인 캐릭터만 조종할 수 있게해줌 (바라보는방향)
        {
            GAnimator.SetBool("Fly", false);
            MouseRotation();
            if(startManger.start1){
            RayCast();
            MouseEvent();
            }
            
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

            if (!this.gameObject.CompareTag("Dead"))
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
                    if (this.gameObject.tag != "Hide")
                    {
                        isOn = !isOn; // 손전등의 상태를 변경합니다.
                        photonView.RPC("ToggleFlashlight", RpcTarget.All, isOn);
                    }

                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (this.gameObject.tag != "Hide")
                    {

                        soundOnOff = !soundOnOff; //유인용 라디오 ON
                        photonView.RPC("ToggleRadioOn", RpcTarget.All, soundOnOff);

                    }

                }
            }
        }
    }
    //기존스크립트에서 추가된 내용 ↓(윤기)
    
    void ChangeLayer(Transform parent) //자식들 레이어바꿔주기 카메라 culling
    {
        // 부모 오브젝트의 자식들에 대해 레이어를 변경합니다.
        foreach (Transform child in parent)
        {
            if (child.name != "GameOver");
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
            FlashLightSound.Play();
        }
        else
        {
            MLight.SetActive(false);
            FlashLightSound.Play();
        }
    }

    
    [PunRPC]
    private void ToggleRadioOn(bool state)
    {
        // 유인용 라디오 소리 On
        if (state)
        {
            audioSource.mute = false;
            FollowingOnOff = true;
            
        }
        else
        { 
            audioSource.mute = true;
           FollowingOnOff  = false;
           
        }

    }
    [PunRPC]
    private void ToggleDoor(bool state){
        if(state){
                        
            Debug.Log(state);
            
            Hanimator.SetBool("Open",true);
            Hanimator.SetBool("Close",false);
            this.gameObject.tag = "Player";
            navmeshlink.gameObject.SetActive(true);
                        
        }
        else{
            Debug.Log(state);
                        
            Hanimator.SetBool("Close",true);
            Hanimator.SetBool("Open",false);
            this.gameObject.tag = "Hide";
            navmeshlink.gameObject.SetActive(false);
            if(soundOnOff){
                photonView.RPC("ToggleRadioOn", RpcTarget.All, state);
            }
            if(isOn){
                photonView.RPC("ToggleFlashlight", RpcTarget.All, state);
            }
        }
    }


    void MouseEvent()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    } 

    
    void RayCast() {
        nowObject_clone = pickUpscript.nowObject;
        ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f,50f));
        selection  = rayHit.transform;
        layerMask = (-1) - (1 << LayerMask.NameToLayer("Player"));
        if(Physics.Raycast (ray, out rayHit,MAX_DISTANCE,layerMask)){
            Debug.DrawLine(ray.origin,rayHit.point,Color.green);
            Debug.Log(rayHit.transform.tag);
            if(rayHit.transform.tag == "Key" && nowObject_clone == null){
                highlight = rayHit.transform;
                if(highlight.gameObject.GetComponent<Outline>() != null)
                {
                     highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                     Outline outline = highlight.gameObject.AddComponent<Outline>();
                     outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.red;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 14.0f;  
                }
                }
                if(highlight != null && highlight != selection)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = false;
                }
        }
        else
        {
             Debug.DrawRay(ray.origin,ray.direction* 100,Color.red);
            
        }
        }
 
        
    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)  
    {
        
    }
    void OnTriggerEnter(Collider other){
       
        if(other.CompareTag("Enemy") && this.gameObject.tag!="Hide"){
            this.gameObject.transform.position = new Vector3(-2.2f,7f,5f);
            StartCoroutine(DelayAndActivate());

        }
    }
    
    void OnTriggerStay(Collider other){
        if(other.CompareTag("Hiding")){
            if(Input.GetKeyDown(KeyCode.V) && !inputproceed){
                openorclose = !openorclose;
                inputproceed = true;
                photonView.RPC("ToggleDoor", RpcTarget.All, openorclose);
                    
            }
        }
    }
    
    private IEnumerator DelayAndActivate(){
        yield return new WaitForSeconds(delay);
        this.gameObject.tag = "Dead";
    }

}
