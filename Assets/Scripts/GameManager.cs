using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameOver = false;

    public GameObject pauseCanvas;

    public void RestartGame()
    {
        isGameOver = false;


        pauseCanvas.SetActive(true);


        ProjectileSpawner spawner = FindObjectOfType<ProjectileSpawner>();
        if (spawner != null)
        {
            spawner.StartSpawning();
        }


    }

    public void OnRestartButtonPressed()
    {
        GameManager.Instance.RestartGame();
    }

    //Det gamla scripted finns här nedan, jag ville testa en sak med projectiles spawner -sofie

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Instance.isGameOver)
            pauseCanvas.SetActive(false);
    }
}

