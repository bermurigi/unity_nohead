using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possible : MonoBehaviour
{
    public bool Show;

    void Update()
    {
        if (Show)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
