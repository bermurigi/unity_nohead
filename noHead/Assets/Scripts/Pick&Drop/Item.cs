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
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}

