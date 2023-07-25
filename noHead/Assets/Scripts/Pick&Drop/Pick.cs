using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    public GameObject nearObject;
    public GameObject nowObject;
    public Transform PlayerTransform;
    bool ItemPick;
    bool MovingItem;

    void Update()
    {
        GetInput();
        PickUp();
        if (MovingItem)
        {
            nowObject.transform.position = PlayerTransform.position;
        }
    }

    void GetInput()
    {
        ItemPick = Input.GetButtonDown("PickUp");
    }

    void PickUp()
    {
        if (ItemPick && nearObject != null)
        {
            nowObject = nearObject;
            Item item = nowObject.GetComponent<Item>();
            if (nearObject.tag == "Key" && MovingItem == false)
            {
                nowObject.transform.position = PlayerTransform.position;
                nowObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                MovingItem = true;
                item.rb.isKinematic = true;
            }
            else if (MovingItem)
            {
                MovingItem = false;
                item.rb.isKinematic = false;
                nowObject = null;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Key" && nearObject == null)
        {
            nearObject = other.gameObject;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Key")
        {
            nearObject = null;
        }
    }
}
