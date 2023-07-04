using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _Instance;
    static Managers Instance { get { Init(); return _Instance; } }
    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    void Update()
    {
        _input.OnUpdate();
        
    }

    void Start()
    {
        Init();
    }
    static void Init()
    {
        if (_Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                //유니티에서 오브젝트를 생성하는 과정을 코드로
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go); //go가 삭제되지 않도록 막아줌
            _Instance = go.GetComponent<Managers>();
        }
    }
}
