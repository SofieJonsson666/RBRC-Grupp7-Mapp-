using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab;

    [SerializeField] private float initialInterval = 5f;
    [SerializeField] private float minimumInterval = 1f;
    [SerializeField] private float decreaseMultiplier = 0.95f;

    private float currentInterval;
    private Coroutine spawnCoroutine;

    void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        currentInterval = initialInterval;

        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);

        spawnCoroutine = StartCoroutine(SpawnProjectiles());
    }

    private IEnumerator SpawnProjectiles()
    {
        while (true)
        {
            if (GameManager.Instance != null && GameManager.Instance.isGameOver)
                yield break;

            Instantiate(BulletPrefab, new Vector3(17, Random.Range(0f, 13f), 0), Quaternion.identity);

            yield return new WaitForSeconds(currentInterval);

            currentInterval = Mathf.Max(minimumInterval, currentInterval * decreaseMultiplier);
        }
    }
}

