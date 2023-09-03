using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Item : MonoBehaviourPun
{
    public enum Type { KeyItem, Item };
    public Type type;
    public int value;
    public Rigidbody rb;
    
    public bool PickingItem;
    private MeshRenderer meshRenderer;
    private bool ischange;
    public int[] playerNum;


    [SerializeField]
    private GameObject startPoint;
    
    

    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();

        playerNum = new int[2];




    }

    private void Update()
    {
        

        if (Open.instance.RandomItem[value]&& ischange==false)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                int[] Numbers = MaterialManager.Instance.GenerateDifferentRandomNumbers();
                //photonView.RPC("PlayerNumberSet", RpcTarget.AllBuffered,Numbers[0],Numbers[1]);
                photonView.RPC("PlayerNumberSet", RpcTarget.AllBuffered, Numbers[0], Numbers[1]);


            }

            if(PhotonNetwork.IsMasterClient)
            {
                
                ChangeMaterial(playerNum[0]);
            }
            else
            {
                ChangeMaterial(playerNum[1]);
            }

            ischange = true;
        }
       
        
    }

    [PunRPC]
    public void PlayerNumberSet(int number0, int number1)
    {
        playerNum[0] = number0;
        playerNum[1] = number1;
    }

   


    [PunRPC]
    public void UpdatePickingItem(bool newValue)
    {
        PickingItem = newValue;
    }
    
    [PunRPC]
    public void UpdateIsKinematic(bool newValue)
    {
        rb.isKinematic = newValue;
    }

    public void ChangeMaterial(int index)
    {
        if (MaterialManager.Instance != null)
        {
            meshRenderer.material = MaterialManager.Instance.sharedMaterials[index];
        }
    }


    void OnTriggerEnter(Collider other){
       
        if(other.CompareTag("MapOutside"))
        {
            this.gameObject.transform.position = Open.instance.startPoint.transform.position;


        }
    }
}

