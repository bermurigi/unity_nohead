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
    public GameObject PlayerObject;
    public bool possibleBurn;
    public bool PickingItem;
    bool BurnItem;

    void Update()
    {
        
        BurnItem = Input.GetButtonDown("PickUp");
        DestroyItem();
        
        
        
        
    }

    void DestroyItem() 
    {
        if (BurnItem && possibleBurn)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Burn")
        {
            possibleBurn = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Burn")
        {
            possibleBurn = false;
        }
    }

    
    
}

