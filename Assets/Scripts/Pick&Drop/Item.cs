using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;

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

    [SerializeField] private GameObject hintLight;
    public bool lightbool;
    public bool flashbool;

    private MeshCollider _meshCollider;
    

    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        type = Type.Item;

        playerNum = new int[2];
        
        hintLight = GetComponentInChildren<Light>().gameObject;
        hintLight.GetComponent<Light>().range = 1f;
        hintLight.GetComponent<Light>().intensity = 0.01f;
        hintLight.GetComponent<Light>().color = new Color(255, 255, 255);
        hintLight.SetActive(false);
        lightbool = false;
        flashbool = true;

        _meshCollider = gameObject.GetComponent<MeshCollider>();
        rb.useGravity = true;
        //rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;;
       
        



    }

    private void Update()
    {
        

        if (Open.instance.RandomItem[value]&& ischange==false)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("KeyItemSet", RpcTarget.AllBuffered);
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
        if (Open.instance.hintStart && type==Type.KeyItem)
        {
            Invoke("FlashEffect",5f);
            photonView.RPC("UpdateHintLight",RpcTarget.AllBuffered,flashbool);
        }

        if (lightbool)
        {
            hintLight.SetActive(true);
        }


       


    }

    public void FlashEffect()
    {
        flashbool = !flashbool;
    }

    [PunRPC]
    public void hintLightOn()
    {
        hintLight.SetActive(true);
    }
    [PunRPC]
    public void KeyItemSet()
    {
        type=Type.KeyItem;
    }

    [PunRPC]
    public void PlayerNumberSet(int number0, int number1)
    {
        playerNum[0] = number0;
        playerNum[1] = number1;
    }

   

    [PunRPC]
    public void UpdateCollider(bool newValue)
    {
        _meshCollider.isTrigger = newValue;
    }
    

    [PunRPC]
    public void UpdatePickingItem(bool newValue)
    {
        PickingItem = newValue;
    }
    [PunRPC]
    public void UpdateHintLight(bool newValue)
    {
        lightbool = newValue;
    }
    
    [PunRPC]
    public void UpdateIsKinematic(bool newValue)
    {
        rb.isKinematic = newValue;
        rb.freezeRotation = newValue;
        if (newValue)
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        else if (!newValue)
            rb.constraints = RigidbodyConstraints.None;


    }
    [PunRPC]
    public void UpdateGravity(bool newValue)
    {
        rb.useGravity = newValue;
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

