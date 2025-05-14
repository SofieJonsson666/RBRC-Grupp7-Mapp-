using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject StruggleEnemyPrefab;

    [SerializeField] private float enemyInterval = 3.5f;
    [SerializeField] private float struggleEnemyInterval = 4f;

    [SerializeField] private float groundY = 20f; // Set this to your ground level
    [SerializeField] private float spawnZ = 0f;

    private int counter;

    private void Awake()
    {
        counter = 0;
    }

    private void Start()
    {
        StartCoroutine(spawnEnemy(enemyInterval, EnemyPrefab));
        //StartCoroutine(spawnEnemy(struggleEnemyInterval, StruggleEnemyPrefab));
    }

    private void Update()
    {
        counter = GameObject.FindGameObjectsWithTag("StruggleEnemy").Length;
        if (counter == 0)
        {
            SpawnStruggleEnemy(StruggleEnemyPrefab);
        }
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        while (true)
        {
            if (GameManager.Instance != null && GameManager.Instance.isGameOver)
                yield break;

            yield return new WaitForSeconds(interval);

            if (GameManager.Instance != null && GameManager.Instance.isGameOver)
                yield break;

            Vector3 spawnPosition = new Vector3(17, groundY, spawnZ);
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }

        /*
        yield return new WaitForSeconds(interval);
        Vector3 spawnPosition = new Vector3(17, groundY, 0);
        Instantiate(enemy, spawnPosition, Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
        */
    }

    private void SpawnStruggleEnemy(GameObject struggleEnemy)
    {
        Vector3 spawnPosition = new Vector3(17, groundY, spawnZ);
        Instantiate(struggleEnemy, spawnPosition, Quaternion.identity);
    }
}