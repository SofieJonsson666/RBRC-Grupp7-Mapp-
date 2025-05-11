using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    [SerializeField] private GameObject SeedPrefab;
    //[SerializeField] private GameObject BreadPrefab;

    [SerializeField] private float seedInterval = 1.5f;
    //[SerializeField] private float breadInterval = 3.5f;

    [SerializeField] private float spawnZ = -1f;
    [SerializeField] private float randBtm;
    [SerializeField] private float randTop;

    void Start()
    {
        StartCoroutine(spawnItems(seedInterval, SeedPrefab));
        //StartCoroutine(spawnItems(breadInterval, BreadPrefab));
    }

    private IEnumerator spawnItems(float interval, GameObject items)
    {
        yield return new WaitForSeconds(interval * Random.Range(randBtm, randTop));

        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            yield break; // Stop spawning if game over

        GameObject newItems = Instantiate(items, new Vector3(17, Random.Range(0f, 13f), spawnZ), Quaternion.identity);
        StartCoroutine(spawnItems(interval, items));
    }
}
