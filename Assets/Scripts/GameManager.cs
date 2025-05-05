using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameOver = false;

    public GameObject pauseCanvas;

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

