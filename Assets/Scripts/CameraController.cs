using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f;

    void FixedUpdate()
    {
        // Scroll right smoothly at a constant speed
        transform.position += Vector3.right * scrollSpeed * Time.fixedDeltaTime;
    }
}

