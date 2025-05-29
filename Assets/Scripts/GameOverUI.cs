using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip gameOverSound;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        if (gameOverSound != null && audioSource != null)
            audioSource.PlayOneShot(gameOverSound);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
