using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUI : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip confirmClip;

    public void PlayConfirmAudio()
    {
        audioSource.PlayOneShot(confirmClip);
    }

}
