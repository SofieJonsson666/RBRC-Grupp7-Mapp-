using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float actualSpeed;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        actualSpeed = speed + DataSaver.instance.mps;

        /*if (GameManager.Instance != null)
            Debug.Log("GameManager exists. isGameOver: " + GameManager.Instance.isGameOver);*/

        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
        {
            rb.velocity = Vector2.zero; 
            return;
        }

        rb.velocity = new Vector2(-actualSpeed, rb.velocity.y); 
    }
}