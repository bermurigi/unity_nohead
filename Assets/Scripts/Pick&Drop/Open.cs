using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Playables;
using UnityEngine.UI;


public class Open : MonoBehaviourPunCallbacks, IPunObservable
{
    public bool[] RandomItem = new bool[5]; 
    // public GameObject DoorObject;
    int rand;
    public int Keycount;

    public static Open instance = null;
    private bool isRand;
    public PlayableDirector EndingAnim;
    public GameObject Enemy;
    AudioSource FireSound;

    public GameObject KeycountText;

    
    public GameObject startPoint; 
    public bool gameStart = false;//힌트주는용도
    public bool hintStart = false;

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

        FireSound = GetComponent<AudioSource>(); 
        
        

    }
    


    [PunRPC]
    void RandItem()
    {
        
            for (int i = 0; i < 11; i++)
            {   //이게 키아이템 뽑는거
                rand = Random.Range(0, 180);
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
    
    /*

    [PunRPC]
    void RPCItem()
    {
        for (int i = 0; i < 1; i++)
        {
            RandomItem[i] = RandomItem[i];
        }
    }
    */
   
   

    void Update()
    {
        
        if(Keycount == 0)
        {
            //���� �ִϸ��̼� ����
            Enemy=GameObject.FindGameObjectWithTag("Enemy");
            Enemy.SetActive(false);
            EndingAnim.Play();
            //Door door = DoorObject.GetComponent<Door>();
            //door.Opendoor = true;
            KeycountText.SetActive(false);
            Keycount = -1;
            
        }

        if (KeycountText == null)
        {
            KeycountText=GameObject.FindWithTag("Count");
            if (KeycountText != null)
            {
                KeycountText.SetActive(false);

            }
        }
        else if (KeycountText != null)
        {
            KeycountText.GetComponent<Text>().text = "남은 수: " + Keycount.ToString();
        }

        /*
        if (PhotonNetwork.IsMasterClient && !isRand) 
        {
            
            //RandItem();
            
        }
        */
    }
    [PunRPC]
    void KeyCountSetActive(bool value)
    {
        
        KeycountText.SetActive(value);
        
        
    }
    [PunRPC]
    void RandItemSet(int i,bool value)
    {
        
            RandomItem[i] = value;
        
        
    }

    
    public void StartRand()
    {
        RandItem();
        for (int i = 0; i < 180; i++)
        {
            photonView.RPC("RandItemSet",RpcTarget.All,i,RandomItem[i]);
        }
        
        isRand = true;
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
            FireSound.Play();
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
