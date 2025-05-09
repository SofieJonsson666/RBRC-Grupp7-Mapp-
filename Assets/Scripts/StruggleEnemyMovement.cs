using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class StruggleEnemyMovement : MonoBehaviour
{
    public StruggleEnemyDetectionZone attackzone;

    [SerializeField] private GameObject struggleBirdPosition;
    [SerializeField] private BoxCollider2D colliderNormal;
    [SerializeField] private BoxCollider2D colliderStruggle;
    [SerializeField] private float speed = 2f;
    private Rigidbody2D rb;
    Animator animator;
    

    public bool _hasTarget = false;
    private bool canMove = true;


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
        struggleBirdPosition.gameObject.SetActive(false);
        colliderNormal.enabled = true;
        colliderStruggle.enabled = false;

    }

    void Update()
    {
        HasTarget = attackzone.detectedColliders.Count > 0; 
    }

    private void FixedUpdate()
    {

        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
        {
            rb.velocity = Vector2.zero; // Stop movement when game is over
            return;
        }

        rb.velocity = new Vector2(-speed, rb.velocity.y); // Maintain vertical velocity for gravity

        if (CanMove && canMove)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.x); // <- This moves the enemy left
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private bool isHit = false;

    public void OnAttack()
    {
        if (!isHit)
        {
            struggleBirdPosition.gameObject.SetActive(true);
            isHit = true;
            canMove = false;
            Debug.Log("Hit enemy!");
            animator.SetTrigger("birdStruggle");
            colliderNormal.enabled = false;
            colliderStruggle.enabled = true;

            //add effects here later
            rb.velocity = Vector2.zero;
            
            //musssiiiiccc
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnAttack();
            collision.gameObject.transform.position = struggleBirdPosition.transform.position;
            Debug.Log("Enemy hit player!");

        }
        if (collision.gameObject.CompareTag("StruggleEnemy"))
        {
            canMove = false;
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
    internal static string birdStruggle = "birdStruggle";
}