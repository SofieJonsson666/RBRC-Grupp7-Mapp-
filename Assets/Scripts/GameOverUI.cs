using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip gameOverSound;

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI currentScoreText;

    private void OnEnable()
    {
        currentScoreText.text = DataSaver.instance.seedAmount.ToString();
        highScoreText.text = DataSaver.instance.highScore.ToString();

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
