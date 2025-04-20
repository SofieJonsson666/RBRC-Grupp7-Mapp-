using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    private bool optionsOpen;
    [SerializeField] private TMP_Text arOnOff;

    private void Start()
    {
        if (DataSaver.instance.ar) arOnOff.text = "ON";
        else arOnOff.text = "OFF";
    }

    public void SceneLoader()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        if (optionsOpen)
        {
            optionsMenu.SetActive(false);
            optionsOpen = false;
            return;
        }

        optionsMenu.SetActive(true);
        optionsOpen = true;
    }

    public void Ar()
    {
        if (DataSaver.instance.ar)
        {
            DataSaver.instance.ar = false;
            arOnOff.text = "OFF";
            return;
        }

        DataSaver.instance.ar = true;
        arOnOff.text = "ON";
    }
}
