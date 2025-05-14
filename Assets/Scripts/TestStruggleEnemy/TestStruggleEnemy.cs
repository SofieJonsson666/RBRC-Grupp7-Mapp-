using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TestStruggleEnemy : MonoBehaviour
{
    public StruggleEnemyDetectionZone attackzone;

    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject struggleBirdPosition;
    [SerializeField] private BoxCollider2D colliderNormal;
    [SerializeField] private BoxCollider2D colliderStruggle;


    private Rigidbody2D rb;
    private Animator animator;

    public bool _hasTarget = false;
    private bool canMove = true;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //struggleBirdPosition.gameObject.SetActive(false);
        colliderNormal.enabled = true;
        colliderStruggle.enabled = false;
    }

    public bool HasTarget { get { return _hasTarget; } private set
        {
            _hasTarget = value;
            animator.SetBool(TestAnimationStrings.hasTarget, value);
        }
            
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(TestAnimationStrings.canMove);
        }

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

    private void OnAttack()
    {
        if (!isHit)
        {
            //struggleBirdPosition.gameObject.SetActive(true);
            Debug.Log("TestStruggleEnemy OnAttack");
            isHit = true;
            canMove = false;
            animator.SetTrigger("birdStruggle");
            colliderNormal.enabled = false;
            colliderStruggle.enabled = true;

            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnAttack();
            collision.gameObject.transform.position = struggleBirdPosition.transform.position;

        }
        if (collision.gameObject.CompareTag("StruggleEnemy"))
        {
            canMove = false;
        }
    }
}
internal class TestAnimationStrings
{
    internal static string hasTarget = "hasTarget";
    internal static string canMove = "canMove";
    internal static string isAlive = "isAlive";
    internal static string lockVelocity = "lockVelocity";
    internal static string hitTrigger = "hitTrigger";
    internal static string birdStruggle = "birdStruggle";
}
