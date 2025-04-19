using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootForce = 10f;
    [SerializeField] private float shootCooldown = 0.2f;
    [SerializeField] private float projectileLifetime = 3f;
    [SerializeField] private float minSwipeDistance = 30f;

    private float lastShootTime;

    private Vector2 swipeStartPos;
    private bool swiping = false;

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x > Screen.width / 2)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    swipeStartPos = touch.position;
                    swiping = true;
                }

                if (touch.phase == TouchPhase.Ended && swiping)
                {
                    Shoot(swipeStartPos, touch.position);
                    swiping = false;
                }
            }
        }
    }

    private void Shoot(Vector2 start, Vector2 end)
    {

        if (Time.time - lastShootTime < shootCooldown)
        {
            return;
        }

        lastShootTime = Time.time;

        Vector2 direction = end - start;

        if (direction.magnitude < minSwipeDistance)
        {
            direction = Vector2.right;
        }
        else
        {
            direction.Normalize();

            if (direction.x <= 0f)
            {
                direction = Vector2.right;
            }

        }

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.1f;
        rb.velocity = direction * shootForce;

        Collider2D projectileCollider = projectile.GetComponent<Collider2D>();
        Collider2D birdCollider = GetComponent<Collider2D>();

        if (projectileCollider != null && birdCollider != null)
        {
            Physics2D.IgnoreCollision(projectileCollider, birdCollider);
        }

        Destroy(projectile, projectileLifetime);
    }


}
