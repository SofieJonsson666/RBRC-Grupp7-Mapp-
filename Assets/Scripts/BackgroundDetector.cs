using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDetector : MonoBehaviour
{
    private Vector3 location = new Vector3(28.5f, 0, 0);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Background"))
        {
            Debug.Log("Entered");
            other.transform.position = location;
        }
    }
}