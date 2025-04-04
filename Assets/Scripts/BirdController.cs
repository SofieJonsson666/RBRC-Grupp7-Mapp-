using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{

    public float velocity = 1f;
    public Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetButton(0))
        {
            rigidBody.velocity = Vector2.up * velocity;
        }
        */
    }
}
