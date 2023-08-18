using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;
    public Material[] sharedMaterials;
    
    
    public int MyNum;

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
