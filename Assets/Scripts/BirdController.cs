using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private float velocity = 5f;
    private Rigidbody2D rigidBody;
    private bool isFlying = false;

    private float minHeight;
    private float maxHeight;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        Camera camera = Camera.main;
        float camHeight = camera.orthographicSize;

        minHeight = camera.transform.position.y - camHeight;
        maxHeight = camera.transform.position.y + camHeight;
    }

    void Update()
    {
        isFlying = Input.GetMouseButton(0) || Input.touchCount > 0;
    }

    void FixedUpdate()
    {
        if (isFlying)
        {
            rigidBody.AddForce(Vector2.up * velocity, ForceMode2D.Impulse);
        }

        // Soft clamp to avoid physics conflict
        if (transform.position.y < minHeight)
        {
            transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        }

        if (transform.position.y > maxHeight)
        {
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        }
    }
}

