using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdaptiveMusicTrigger : MonoBehaviour
{
    private AdaptiveMusicPlayer musicManager;

    void Start()
    {
        musicManager = FindObjectOfType<AdaptiveMusicPlayer>();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        switch (sceneIndex)
        {
            case 1: //character selection
                musicManager?.FadeInBass();
                break;
            case 2: //main game
                musicManager?.FadeInBass();
                musicManager?.FadeInMelody();
                break;
            case 3: //Stats scene
                musicManager?.FadeInBass();
                break;
            case 4: //Birdselfie
                musicManager?.FadeInBass();
                break;
            case 5: //Tutorial
                musicManager?.FadeInBass();
                break;

        }
    }
}

