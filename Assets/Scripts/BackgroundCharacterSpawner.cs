using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] backgroundPrefabs; 
    [SerializeField] private float spawnInterval = 5f; 
    [SerializeField] private float spawnX = 15f; 
    [SerializeField] private float minY = 1f;
    [SerializeField] private float maxY = 5f;

    private void Start()
    {
        StartCoroutine(SpawnCharacters());
    }

    private IEnumerator SpawnCharacters()
    {
        while (true)
        {
            if (GameManager.Instance != null && GameManager.Instance.isGameOver)
                yield break;

            yield return new WaitForSeconds(spawnInterval);

            if (GameManager.Instance != null && GameManager.Instance.isGameOver)
                yield break;

            SpawnCharacter();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnCharacter()
    {
        int index = Random.Range(0, backgroundPrefabs.Length);
        Vector3 spawnPosition = new Vector3(spawnX, Random.Range(minY, maxY), 0f);
        Instantiate(backgroundPrefabs[index], spawnPosition, Quaternion.identity);
    }
}
