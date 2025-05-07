using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("EnemyProjectileAnimation_Fly");
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            return;

        transform.Translate(new Vector2(-speed, 0) * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Debug.Log("projectile hit bird!");
            

        }
    }
}
