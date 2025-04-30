using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StruggleEnemyMovement : MonoBehaviour
{
    public StruggleEnemyDetectionZone attackzone;

    [SerializeField] private float speed = 2f;
    private Rigidbody2D rb;
    Animator animator;

    public bool _hasTarget = false;


    public bool HasTarget { get { return _hasTarget; } private set {
        _hasTarget = value;
        animator.SetBool(AnimationStrings.hasTarget, value);
        
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
    }
}

internal class AnimationStrings
{
    internal static string hasTarget = "hasTarget";
}