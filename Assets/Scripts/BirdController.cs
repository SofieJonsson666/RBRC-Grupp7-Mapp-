using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{

    private float velocity = 5f;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {

            rigidBody.AddForce(Vector2.up * velocity, ForceMode2D.Force);

            //rigidBody.velocity = Vector2.up * velocity;

        }

    }
}
