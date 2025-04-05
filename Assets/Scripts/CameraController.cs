using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float scrollSpeed = 2f;
    [SerializeField] private Transform bird;

    private float verticalFollowSpeed = 5f;
    private float verticalOffset = 0f;

    private void LateUpdate()
    {
        
        transform.position += Vector3.right * scrollSpeed * Time.deltaTime;

        
        Vector3 newPos = transform.position;
        newPos.y = Mathf.Lerp(transform.position.y, bird.position.y + verticalOffset, verticalFollowSpeed * Time.deltaTime);
        transform.position = newPos;
    }
}
