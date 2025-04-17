using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StruggleEnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void FixedUpdate()
    {
        transform.Translate(new Vector2(-speed, 0) * Time.deltaTime);
    }
}
