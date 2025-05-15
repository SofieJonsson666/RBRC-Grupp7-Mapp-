using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    public GameObject bigBoom;
    public GameObject boomParticle;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Instantiate(boomParticle, collision.gameObject.transform.position, Quaternion.identity);
            //Instantiate(bigBoom, collision.gameObject.transform.position, Quaternion.identity);
        }
    }
}
