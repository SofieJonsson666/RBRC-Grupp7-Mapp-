using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveMusicPlayer : MonoBehaviour
{
    [SerializeField] public AudioSource drums;
    [SerializeField] public AudioSource bass;
    [SerializeField] public AudioSource melody;

    private static AdaptiveMusicPlayer instance;

    

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    public void ResetAndPlay()
    {
        drums.volume = 0.1f;
        bass.volume = 0f;
        melody.volume = 0f;

        if (!drums.isPlaying) drums.Play();
        if (!bass.isPlaying) bass.Play();
        if (!melody.isPlaying) melody.Play();
    }

    private void Start()
    {
        
        drums.Play();
        bass.Play();
        melody.Play();

        
        drums.volume = 0.1f;
        bass.volume = 0f;
        melody.volume = 0f;
    }

    public void StopAllMusic()
    {
        drums.Stop();
        bass.Stop();
        melody.Stop();
    }

    public void FadeInBass(float duration = 2f)
    {
        StartCoroutine(FadeIn(bass, duration));
    }

    public void FadeInMelody(float duration = 2f)
    {
        StartCoroutine(FadeIn(melody, duration));
    }

    private IEnumerator FadeIn(AudioSource source, float duration)
    {
        float time = 0f;
        float startVolume = source.volume;
        float targetVolume = 0.1f;

        while (time < duration)
        {
            source.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        source.volume = targetVolume;

        
    }
}
