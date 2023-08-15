using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item : MonoBehaviourPun
{
    public enum Type { KeyItem };
    public Type type;
    public int value;
    public Rigidbody rb;
    
    public bool PickingItem;
    private MeshRenderer meshRenderer;
   

    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        int[] Numbers = MaterialManager.Instance.GenerateDifferentRandomNumbers();
        
        if(PhotonNetwork.IsMasterClient)
        {
            ChangeMaterial(Numbers[0]);
        }
        else
        {
            ChangeMaterial(Numbers[1]);
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

