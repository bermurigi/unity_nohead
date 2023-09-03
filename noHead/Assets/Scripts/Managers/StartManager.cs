using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
public class StartManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private GameObject StartCanvas;
    [SerializeField] private Text StartText;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject StartPoint;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject StartBgm;

    public GameObject ExplanationCanvas;
    public AudioSource startBell;

    

    public bool start1;
    
    public float delayInSeconds; //힌트지연시간
    
    private void start(){
        start1 = false;
    }

   
   
    private void Update()
    {
        int playerCount = PhotonNetwork.PlayerList.Length;
        StartText.text = "현재 인원 : " + playerCount + "명" + "(" + playerCount + "/2)";
        if (playerCount >= 1 && PhotonNetwork.IsMasterClient)
        {
            StartButton.SetActive(true);
            
        }
        else
        {
            StartButton.SetActive(false);
        }

        
    }

    [PunRPC]
    void EnemySpawn()
    {
        //Enemy.SetActive(true);
        

    }

    public void ExplanationButtonClick()
    {
        ExplanationCanvas.SetActive(true);
    }
    
    public void ExplanationCancleButtonClick()
    {
        ExplanationCanvas.SetActive(false);
    }

    

    
    public void StartButtonClick()
    {
        photonView.RPC("start1Equal",RpcTarget.All);
        photonView.RPC("MovePlayerToStartPoint", RpcTarget.All);
        photonView.RPC("DeleteStartCanvas", RpcTarget.All);
        
        Open.instance.StartRand();
        Open.instance.gameStart = true;
        Open.instance.KeycountText.SetActive(true);
        startBell.Play();
        
        
        Invoke("SpawnEnemyDelayed", 10.0f);
        StartCoroutine(StartHintCoroutine());
        
    }
    private IEnumerator StartHintCoroutine()
    {
        // delayInSeconds 만큼 대기합니다.
        yield return new WaitForSeconds(delayInSeconds);

        // hintStart를 true로 설정합니다.
        Open.instance.hintStart = true;

        // 다른 작업을 수행할 수 있습니다.
    }

    [PunRPC]
    void start1Equal()
    {
        start1 = true;
    }
    
    [PunRPC]
    void SpawnEnemyDelayed()
    {
        photonView.RPC("EnemySpawn",RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void MovePlayerToStartPoint()//거의다함 여기건들이면끝날듯
    {
        BgmStart Off = StartBgm.GetComponent<BgmStart>();
        Off.OffBgm = true;
        // 플레이어 오브젝트를 새 위치로 이동시킴
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
       
        
        foreach (GameObject player in players)
        {
            if (player != null)
            {
                player.transform.position = StartPoint.transform.position;
            }
        }
        
        

    }

    [PunRPC]
    private void DeleteStartCanvas()
    {
        StartCanvas.SetActive(false);
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)  
    {
        
    }
}
