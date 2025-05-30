using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float destroyX = -20f;

    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
