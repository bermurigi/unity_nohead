using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    public GameObject nearObject;
    public bool[] hasItems;
    public GameObject[] CopyItem;
    public Transform PlayerTransform;
    bool ItemPick;
    bool ItemDrop;

    void Update()
    {
        GetInput();
        PickUp();
        Drop();
    }

    void GetInput()
    {
        ItemPick = Input.GetButtonDown("PickUp");
        ItemDrop = Input.GetButtonDown("Drop");
    }

    void Drop()
    {
        if(ItemDrop && hasItems[0])
        {
            Instantiate(CopyItem[0], PlayerTransform.position, PlayerTransform.rotation);
            hasItems[0] = false;
        }
    }

    void PickUp()
    {
        if (ItemPick && nearObject != null)
        {
            if (nearObject.tag == "Key")
            {
                Item item = nearObject.GetComponent<Item>();
                int itemIndex = item.value;
                hasItems[itemIndex] = true;

                Destroy(nearObject);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Key")
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
