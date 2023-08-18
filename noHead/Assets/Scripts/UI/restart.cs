using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Script : MonoBehaviour
{
    public PlayableDirector EndingAnim;
    public GameObject chra;
    public GameObject chra2;
    public GameObject endingcamera;

    public void OnClick()
    {
        EndingAnim.Stop();
        chra.SetActive(false);
        chra2.SetActive(false);
        endingcamera.SetActive(false);
    }
   
}
