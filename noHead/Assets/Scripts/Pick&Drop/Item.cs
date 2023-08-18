using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item : MonoBehaviourPun
{
    public enum Type { KeyItem, Item };
    public Type type;
    public int value;
    public Rigidbody rb;
    
    public bool PickingItem;
    private MeshRenderer meshRenderer;
    private bool ischange;
   

    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        
        
        


    }

    private void Update()
    {
        

        if (Open.instance.RandomItem[value]&& ischange==false)
        {
            int[] Numbers = MaterialManager.Instance.GenerateDifferentRandomNumbers();
            if(PhotonNetwork.IsMasterClient)
            {
                ChangeMaterial(Numbers[0]);
            }
            else
            {
                ChangeMaterial(Numbers[1]);
            }

            ischange = true;
        }
        
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

    

    
    
}

