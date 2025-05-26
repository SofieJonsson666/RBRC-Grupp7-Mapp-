using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] backgroundPrefabs; // Your 6 character prefabs
    [SerializeField] private float spawnInterval = 5f; // Time between spawns
    [SerializeField] private float spawnX = 15f; // Fixed X position to spawn at
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
