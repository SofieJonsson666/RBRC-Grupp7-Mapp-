using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private float velocity = 20f;
    private Rigidbody2D rigidBody;
    private bool isFlying = false;

    private float minHeight;
    private float maxHeight;

    private float fixedXOffset = 0f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        Camera camera = Camera.main;
        float camHeight = camera.orthographicSize;

        minHeight = camera.transform.position.y - camHeight;
        maxHeight = camera.transform.position.y + camHeight;

        float camWidth = camHeight * Camera.main.aspect;
        fixedXOffset = Camera.main.transform.position.x - camWidth * 0.4f; // 40% from left
    }


    void Update()
    {
        isFlying = Input.GetMouseButton(0) || Input.touchCount > 0;
    }

    void FixedUpdate()
    {
        if (isFlying)
        {
            rigidBody.AddForce(Vector2.up * velocity, ForceMode2D.Force);
        }

        // Stop bird from going below the screen
        if (transform.position.y <= minHeight && rigidBody.velocity.y < 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        }

        // Stop bird from going above the screen
        if (transform.position.y >= maxHeight && rigidBody.velocity.y > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        }

        float camWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float desiredX = Camera.main.transform.position.x - camWidth * 0.4f;

        transform.position = new Vector3(desiredX, transform.position.y, transform.position.z);



    }
}
