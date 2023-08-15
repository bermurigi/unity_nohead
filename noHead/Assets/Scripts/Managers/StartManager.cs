using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject StartCanvas;
    [SerializeField] private Text StartText;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject StartPoint;
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
        
        
    }

    

    /*
    public void StartButtonClick()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // 모든 플레이어의 PhotonView를 가져와서 RPC 메서드 호출
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                PhotonView playerPhotonView = player.TagObject as PhotonView;
                if (playerPhotonView != null)
                {
                    playerPhotonView.RPC("MovePlayerToStartPoint", player);
                }
            }
        }
    }

    [PunRPC]
    private void MovePlayerToStartPoint()
    {
        // 플레이어 오브젝트를 StartPoint로 이동시킴
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = StartPoint.transform.position;
        }
    }
    */
}
