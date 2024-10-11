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
    [SerializeField] private GameObject StartPoint1;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject StartBgm;

    public GameObject ExplanationCanvas;
    public AudioSource startBell;

    public GameObject CreditCanvas;
    

    public bool start1;
    
    public float delayInSeconds; //힌트지연시간
    
    
    
    private void start(){
        start1 = false;
    }

   
   
    private void Update()
    {
        int playerCount = PhotonNetwork.PlayerList.Length;
        StartText.text = "현재 인원 : " + playerCount + "명" + "(" + playerCount + "/2)";
        if (playerCount >= 2 && PhotonNetwork.IsMasterClient)
        {
            StartButton.SetActive(true);
            
        }
        else
        {
            StartButton.SetActive(false);
        }

        
        if (Input.GetKeyUp(KeyCode.G) && !start1)
        {
            /*
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");



            if (players[0] != null)
            {
                players[0].GetComponent<GhostMotionController>().callReady(true);
            }

            
            if (players[1] != null)
            {

                players[1].GetComponent<GhostMotionController>().callReady(true);
            }
            */

            StartButtonClick();
            


        }   

        
    }

    [PunRPC]
    void EnemySpawn()
    {
        Enemy.SetActive(true);
        startBell.Play();

    }

    public void ExplanationButtonClick()
    {
        ExplanationCanvas.SetActive(true);
    }
    
    public void ExplanationCancleButtonClick()
    {
        ExplanationCanvas.SetActive(false);
    }
    public void CreditButtonClick()
    {
        CreditCanvas.SetActive(true);
    }
    
    public void CreditCancleButtonClick()
    {
        CreditCanvas.SetActive(false);
    }

    

    
    public void StartButtonClick()
    {
        photonView.RPC("start1Equal",RpcTarget.All);
        //photonView.RPC("MovePlayerToStartPoint", RpcTarget.AllBuffered);
        photonView.RPC("DeleteStartCanvas", RpcTarget.All);
        
        Open.instance.StartRand();
        Open.instance.gameStart = true;
        Open.instance.photonView.RPC("KeyCountSetActive",RpcTarget.AllBuffered,true);
       //버튼을클릭한사람
        
        //Invoke("plzStop",2f);
        Invoke("SpawnEnemyDelayed", 15.0f);
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


        if (players[0]!=null)
        {
            players[0].transform.position = StartPoint.transform.position;
        }
        else if (players[1] != null)
        {
            players[1].transform.position = StartPoint1.transform.position;
        }
        
        
       
        
        /*
        foreach (GameObject player in players)
        {
            if (player != null)
            {
                players.transform.position = StartPoint.transform.position;
                play
            }
        }
        */
        
        

    }

    public void plzStop()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players[0] != null)
        {
            players[0].GetComponent<GhostMotionController>().callReady(false);
        }

            
        if (players[1] != null)
        {

            players[1].GetComponent<GhostMotionController>().callReady(false);
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
