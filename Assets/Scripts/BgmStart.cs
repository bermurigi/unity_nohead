using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmStart : MonoBehaviour
{
    public bool OffBgm;

    void Update()
    {
        if (OffBgm)
        {
            this.gameObject.SetActive(false);
        }
    }
}
