using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject player;
    public Canvas canvas;

  
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("게임오버 스크립트 작동");
        if(player.CompareTag("Dead")){
            Debug.Log("플레이어 사망");

            ActiveCanvas();
        }
    }
    public void ActiveCanvas(){
        canvas.enabled = true;
    }

    public void DeactivecCanvas(){
        canvas.enabled = false;
    }
}
