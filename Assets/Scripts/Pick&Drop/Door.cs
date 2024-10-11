using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool Opendoor;

    void Update()
    {
        if(Opendoor == true)
        {
            Destroy(this.gameObject);
            Opendoor = false;
        }
    }
}
