using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    private float speed;

    private Rigidbody rb;

    private void Start()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("BackgroundController");
        speed = controller.GetComponent<BackgroundController>().speed;
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(-speed, 0, 0);
    }
}