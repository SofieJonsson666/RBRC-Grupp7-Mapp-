using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    //[SerializeField] private AudioClip pickupSound;
    private Rigidbody2D rb;
    //private AudioSource audioSource;
    //private bool hasCollided = false;

    /*private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }*/

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            return;

        float itemSpeed = speed * DataSaver.instance.mps;
        transform.Translate(new Vector2(-itemSpeed, 0) * Time.deltaTime);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasCollided) return;

        if (collision.CompareTag("Player"))
        {
            hasCollided = true;
            if (pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            // Delay destruction to allow sound to play
            Destroy(gameObject, pickupSound.length);
        }
    }*/
}
