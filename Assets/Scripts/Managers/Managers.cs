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
                //����Ƽ���� ������Ʈ�� �����ϴ� ������ �ڵ��
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go); //go�� �������� �ʵ��� ������
            _Instance = go.GetComponent<Managers>();
        }
    }
}
