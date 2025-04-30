using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject BulletPrefab;
    

    [SerializeField]
    private float projectileInterval = 3.5f;
    

    void Start()
    {
        StartCoroutine(spawnProjectiles(projectileInterval, BulletPrefab));
        
    }

    private IEnumerator spawnProjectiles(float interval, GameObject projectiles)
    {
        yield return new WaitForSeconds(interval);
        GameObject newProjectiles = Instantiate(projectiles, new Vector3(17, Random.Range(0f, 13f), 0), Quaternion.identity);
        StartCoroutine(spawnProjectiles(interval, projectiles));
    }
}
