using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
