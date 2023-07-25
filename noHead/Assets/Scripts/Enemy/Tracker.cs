using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TeleportToClosestPlayer : MonoBehaviour
{
    public string playerTag = "Player";
    public string enemyTag = "Enemy";
    private Transform playerTransform;
    private Transform closestPlayerTransform;
    private NavMeshAgent agent;
    private bool isTeleporting;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        FindClosestPlayerWithEnemy();

        if (playerTransform != null && closestPlayerTransform != null)
        {
            if (!isTeleporting)
            {
                // 순간이동 시작
                StartCoroutine(TeleportToClosestPlayerCoroutine());
            }
        }
        else
        {
            // 플레이어나 가까운 플레이어가 없는 경우 순간이동 중지
            if (isTeleporting)
            {
                StopCoroutine(TeleportToClosestPlayerCoroutine());
                isTeleporting = false;
            }
        }
    }

    private void FindClosestPlayerWithEnemy()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        Transform closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            bool isPlayerClosestWithEnemy = false;
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(player.transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlayer = player.transform;
                    isPlayerClosestWithEnemy = true;
                }
            }

            // 가장 가까운 플레이어가 적 태그를 가진 대상과 가장 가까운 경우 타겟으로 설정
            if (isPlayerClosestWithEnemy)
            {
                playerTransform = player.transform;
                closestPlayerTransform = closestPlayer;
            }
        }
    }

    private IEnumerator TeleportToClosestPlayerCoroutine()
    {
        isTeleporting = true;
        while (playerTransform != null && closestPlayerTransform != null)
        {
            // 추적자를 가장 가까운 플레이어 태그가진 대상에게 순간이동시킵니다.
            transform.position = closestPlayerTransform.position;
            yield return null;
        }
        isTeleporting = false;
    }
}