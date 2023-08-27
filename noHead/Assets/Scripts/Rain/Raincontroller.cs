using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raincontroller : MonoBehaviour
{


    private StartManager startManger;
    private GameObject startMangerobject;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        startMangerobject = GameObject.Find("StartManager");
        startManger = startMangerobject.GetComponent<StartManager>();
        audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startManger.start1)
        {
            audio.mute = true;
        }
        
    }
}
