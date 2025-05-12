using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AudioClip audioClip;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("EnemyProjectileAnimation_Fly");
        audioSource = GetComponent<AudioSource>();


        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            return;

        float projectileSpeed = speed * DataSaver.instance.mps;

        transform.Translate(new Vector2(-projectileSpeed, 0) * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("projectile hit bird!");
        }
    }
}