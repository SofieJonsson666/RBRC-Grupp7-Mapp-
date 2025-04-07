using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControllerVer2 : MonoBehaviour
{

    private float flapForce = 5f;
    private float gravityScale = 3f;

    [SerializeField] private float floorY = 1.1f;

    private Rigidbody2D rigidBody;

    private float minHeight;
    private float maxHeight;

    private float verticalPadding = 0.8f;

    private float horizontalOffsetPercent = 0.1f;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        /*
        Camera camera = Camera.main;
        float camHeight = camera.orthographicSize;

        float padding = 0.8f;

        minHeight = camera.transform.position.y - camHeight + padding;
        maxHeight = camera.transform.position.y + camHeight - padding;

        float camWidth = camHeight * Camera.main.aspect;
        */
        UpdateScreenBounds();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            rigidBody.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {

        UpdateScreenBounds();

        float padding = 0.1f;

        /*
        // Stop bird from going below the screen
        if (transform.position.y <= minHeight && rigidBody.velocity.y < 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            //rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            //transform.position = new Vector3(transform.position.x, floorY + padding, transform.position.z);
        }
        */
        if (transform.position.y <= floorY + 0.01f && rigidBody.velocity.y < 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        }

        // Stop bird from going above the screen
        if (transform.position.y >= maxHeight && rigidBody.velocity.y > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        }

        //float camWidth = Camera.main.orthographicSize * Camera.main.aspect;
        //float desiredX = Camera.main.transform.position.x - camWidth * 0.7f;
        float desiredX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * horizontalOffsetPercent, 0f, GetCameraZDistance())).x;

        transform.position = new Vector3(desiredX, transform.position.y, transform.position.z);



    }

    private void UpdateScreenBounds()
    {
        float camZ = GetCameraZDistance();

        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, camZ));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, camZ));

        minHeight = bottomLeft.y + verticalPadding;
        maxHeight = topRight.y - verticalPadding;
    }

    private float GetCameraZDistance()
    {
        return Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
    }



}
