using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdReaction : MonoBehaviour
{
    private Animator animator = null;

    private AudioSource audioSource = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ReactionHandler()
    {
        animator.SetTrigger("spin");
        audioSource.Play();
    }
}