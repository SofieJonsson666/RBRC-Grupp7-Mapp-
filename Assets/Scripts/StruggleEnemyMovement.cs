using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(Damageable))]
public class StruggleEnemyMovement : MonoBehaviour
{
    public StruggleEnemyDetectionZone attackzone;

    [SerializeField] private float speed = 2f;
    private Rigidbody2D rb;
    Animator animator;
    Damageable damageable;

    public bool _hasTarget = false;


    public bool HasTarget { get { return _hasTarget; } private set {
        _hasTarget = value;
        animator.SetBool(AnimationStrings.hasTarget, value);
        
        } 
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HasTarget = attackzone.detectedColliders.Count > 0; 
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y); // Maintain vertical velocity for gravity

        if (CanMove)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.x); // <- This moves the enemy left
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private bool isHit = false;

    public void OnHit()
    {
        if (!isHit)
        {
            isHit = true;
            Debug.Log("Hit enemy!");
            animator.SetTrigger("isHit");

            //add effects here later
            rb.velocity = Vector2.zero;
            
            //musssiiiiccc
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            OnHit();
            Debug.Log("You hit him!");
            
        }
    }
}



internal class AnimationStrings
{
    internal static string hasTarget = "hasTarget";
    internal static string canMove = "canMove";
    internal static string isAlive = "isAlive";
    internal static string lockVelocity = "lockVelocity";
    internal static string hitTrigger = "hitTrigger";
}