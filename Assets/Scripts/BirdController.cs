using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{

    private float velocity = 20f;
    private Rigidbody2D rigidBody;
    private bool isFlying = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        isFlying = Input.GetMouseButton(0) || Input.touchCount > 0;


    }

    void FixedUpdate()
    {

        if (isFlying)
        {

            rigidBody.AddForce(Vector2.up * velocity, ForceMode2D.Force);

            //rigidBody.velocity = Vector2.up * velocity;

        }

    }
}
