using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;

    private Rigidbody rb;

    private void Start()
    {
        speed = DataSaver.instance.mps;

        GameObject controller = GameObject.FindGameObjectWithTag("BackgroundController");
        //speed = controller.GetComponent<BackgroundController>().speed;
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(-speed, 0, 0);
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
        {
            rb.velocity = Vector3.zero; // stop movement
        }
        else
        {
            speed = DataSaver.instance.mps;
            rb.velocity = new Vector3(-speed, 0, 0);
        }
    }
}