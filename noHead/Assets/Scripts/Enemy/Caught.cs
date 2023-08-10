using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caught : MonoBehaviour
{
    public GameObject JumpCam;
    // Start is called before the first frame update
    void Start()
    {
     JumpCam.SetActive(false);   
    }
    void OnTriggerEnter(Collider other){
        Vector3 Pos = transform.position;
        if(other.CompareTag("Player")){
        // JumpCam.transform.position = Pos;
        Debug.Log("현재 위치 : "+Pos);
        JumpCam.SetActive(true);
        StartCoroutine(end());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator end(){
        yield return new WaitForSeconds(2.03f);
        JumpCam.SetActive(false);
    }
    
}
