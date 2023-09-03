using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadein : MonoBehaviour
{
    private RawImage ri;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        ri = GetComponent<RawImage>();
        ri.color = new Color(1,1,1,0);
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("페이드인 작동");
        if(player.CompareTag("Dead")){
            // Debug.Log("내부함수 작동");
            StartCoroutine(FadeIn());
        }
    }
    IEnumerator FadeIn(){
        Color cTemp = ri.color;
        while(cTemp.a<1){
            cTemp.a +=0.1f;
            ri.color = cTemp;
            yield return null;
        }
    }
}
