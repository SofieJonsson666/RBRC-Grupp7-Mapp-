using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void FixedUpdate()
    {

        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            return;

        transform.Translate(new Vector2 (-speed, 0) * Time.deltaTime);
    }

}
