using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open2 : MonoBehaviour
{
    public GameObject BurnObject;
    AudioSource FireSound;

    void Start()
    {
        FireSound = GetComponent<AudioSource>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Key")
        {
            Item item = other.GetComponent<Item>();
            Open open = BurnObject.GetComponent<Open>();
            if (open.RandomItem[item.value] == true)
            {
                open.RandomItem[item.value] = false;
                open.Keycount--;
            }
            FireSound.Play();
            Destroy(other.gameObject);
        }

    }
}
