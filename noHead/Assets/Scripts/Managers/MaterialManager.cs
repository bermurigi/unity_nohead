using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;
    public Material[] sharedMaterials;


    public int MyNum;

    //ending Material 세팅
    private GameObject[] endingGhost;
    Open IsOpen;
    GhostMotionController[] Ghosts;

    public GameObject[] endingGhostParent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //ending Material 세팅
        IsOpen = FindObjectOfType<Open>();
    }

    //ending Material 세팅
    private void Update()
    {
        if (IsOpen.Keycount == 0)
        {
            endingGhostParent[0].SetActive(true);
            endingGhostParent[1].SetActive(true);
            endingGhost = GameObject.FindGameObjectsWithTag("endingGhost");
            Ghosts = FindObjectsOfType<GhostMotionController>();
            int i = 0;
            foreach (var ghost in Ghosts)
            {
                endingGhost[i].GetComponent<SkinnedMeshRenderer>().material = ghost.mat[ghost.i];
                ghost.gameObject.SetActive(false);
                i++;
            }
        }
    }

    // 서로 다른 랜덤한 숫자 2개를 반환하는 함수
    public int[] GenerateDifferentRandomNumbers()
    {
        int[] randomNumbers = new int[2];

        // 0부터 6까지의 서로 다른 랜덤한 숫자를 생성하여 배열에 저장
        randomNumbers[0] = Random.Range(0, 6);

        do
        {
            randomNumbers[1] = Random.Range(0, 6);
        } while (randomNumbers[1] == randomNumbers[0]); // 서로 다른 값이 나올 때까지 반복

        return randomNumbers;
    }
}