using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject StruggleEnemyPrefab;

    [SerializeField] private float enemyInterval = 3.5f;
    [SerializeField] private float struggleEnemyInterval = 3.5f;

    [SerializeField] private float groundY = 20f; // Set this to your ground level

    void Start()
    {
        StartCoroutine(spawnEnemy(enemyInterval, EnemyPrefab));
        StartCoroutine(spawnEnemy(struggleEnemyInterval, StruggleEnemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        Vector3 spawnPosition = new Vector3(17, groundY, 0);
        Instantiate(enemy, spawnPosition, Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}

