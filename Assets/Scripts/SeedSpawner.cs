
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    public float seedInterval = 3f;  

    [SerializeField] private GameObject SeedPrefab;

    [Header("Spawn Position Settings")]
    [SerializeField] private float spawnX = 17f;
    [SerializeField] private float spawnZ = -1f;
    [SerializeField] private float minY = 1f;
    [SerializeField] private float maxY = 5f;

    private Coroutine spawnCoroutine;

    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnItems());
    }

    private IEnumerator SpawnItems()
    {
        while (true)
        {
            if (GameManager.Instance != null && GameManager.Instance.isGameOver)
                yield break;

            float randomY = UnityEngine.Random.Range(minY, maxY);
            Vector3 spawnPosition = new Vector3(spawnX, randomY, spawnZ);
            Instantiate(SeedPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(seedInterval);
        }
    }


    //[SerializeField] private GameObject SeedPrefab;
    //[SerializeField] private GameObject BreadPrefab;

    //[SerializeField] private float seedInterval = 1.5f;
    //[SerializeField] private float breadInterval = 3.5f;

    /*[SerializeField] private float spawnZ = -1f;
    [SerializeField] private float spawnX = 1f;
    [SerializeField] private float spawnY = 5f;
    [SerializeField] private float randBtm;
    [SerializeField] private float randTop;


    void Start()
    {
        StartCoroutine(spawnItems(seedInterval, SeedPrefab));
        //StartCoroutine(spawnItems(breadInterval, BreadPrefab));
    }

    private IEnumerator spawnItems(float interval, GameObject items)
    {
        yield return new WaitForSeconds(interval * Random.Range(spawnX,spawnY, 0f));

        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            yield break; 

        GameObject newItems = Instantiate(items, new Vector3(17, Random.Range(0f, 13f), spawnZ), Quaternion.identity);
        //StartCoroutine(spawnItems(interval, items));


    }*/
}
