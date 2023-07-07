using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;   
//윤기 작업
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField NickNameInput;
    public GameObject DisconnectPanel;

    private void Awake()
    {
        Screen.SetResolution(960,540,false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();   //커넥트하면 바로 서버에 접속  

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);
    }

    public override void OnJoinedRoom()
    {
        DisconnectPanel.SetActive(false);
        spawn();
    }

    void Update()
    {
        if(PhotonNetwork.IsConnected)
            Debug.Log("Connect");
        if(Input.GetKeyDown(KeyCode.Escape)&&PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        DisconnectPanel.SetActive(true);
        
    }

    public void spawn()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        
    }
    
}
