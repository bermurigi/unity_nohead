using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Playables;


public class Open : MonoBehaviourPunCallbacks, IPunObservable
{
    public bool[] RandomItem = new bool[5]; 
    // public GameObject DoorObject;
    int rand;
    public int Keycount = 3;

    public static Open instance = null;
    private bool isRand;
    public PlayableDirector EndingAnim;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            
        }
        else
        {
            Destroy(this.gameObject);
        }

        isRand = false;



    }


    [PunRPC]
    void RandItem()
    {
        
            for (int i = 0; i < 3; i++)
            {
                rand = Random.Range(0, 4);
                if (RandomItem[rand] == false)
                {
                    RandomItem[rand] = true;
                }
                else
                {
                    i--;
                }
            }
            
        
    }
   
   

    void Update()
    {
        
        if(Keycount == 0)
        {
            //���� �ִϸ��̼� ����
            EndingAnim.Play();
            //Door door = DoorObject.GetComponent<Door>();
            //door.Opendoor = true;
            Keycount = -1;
        }

        if (PhotonNetwork.IsMasterClient && isRand == false) 
        {
            photonView.RPC("RandItem",RpcTarget.AllBuffered);
            isRand = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Key")
        {
            Item item = other.GetComponent<Item>();
            if (RandomItem[item.value] == true) 
            {
                RandomItem[item.value] = false;
                Keycount--;
            }
           
            Destroy(other.gameObject);
        }

    }

    /*
    [PunRPC]
    void DestroyObject(GameObject gameObject)
    {
        Destroy(gameObject);
    }
    */
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)  
    {
        
    }
    
}