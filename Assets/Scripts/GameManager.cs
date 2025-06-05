using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameOver = false;

    public GameObject pauseCanvas;

    public int currentScore = 0;



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

