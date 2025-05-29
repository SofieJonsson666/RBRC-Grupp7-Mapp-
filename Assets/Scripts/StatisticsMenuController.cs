using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatisticsMenuController : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private Button resetSaveButton;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (resetSaveButton != null)
        {
            resetSaveButton.onClick.RemoveAllListeners();
            resetSaveButton.onClick.AddListener(() =>
            {
                if (DataSaver.instance != null)
                {
                    DataSaver.instance.ResetGameData();
                }
            });
        }
        else
        {
            Debug.LogWarning("Reset Save Button not assigned in the Inspector.");
        }
    }

    public void BackToMainMenu()
    {
        Debug.Log("HELLO WE SHOULD GO BACK NOW");
        SceneManager.LoadScene(0);
    }
}