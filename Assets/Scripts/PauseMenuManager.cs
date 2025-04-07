using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private int levelToLoad;
    private bool isPaused;

    void Update()
    {
        if (isPaused)
        {
            Debug.Log("Paused");
        }
        else
        {
            Debug.Log("Not Paused");
        }
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void Home()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
