using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    private bool optionsOpen;
    [SerializeField] private GameObject tutorialMenu;
    private bool tutorialOpen;
    [SerializeField] private TMP_Text arOnOff;
    [SerializeField] private TMP_Text voicecontrolOnOff;

    private void Start()
    {
        if (arOnOff != null)
        {
            if (DataSaver.instance.useCameraBackground)
            {
                arOnOff.text = "ON";
            }
            else
            {
                arOnOff.text = "OFF";
            }
        }

        if (voicecontrolOnOff != null)
        {
            if (DataSaver.instance.voicecontrol)
            {
                voicecontrolOnOff.text = "ON";
            }
            else
            {
                voicecontrolOnOff.text = "OFF";
            }
        }
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

    public void Stats()
    {
        SceneManager.LoadScene(3);
    }

    public void Ar()
    {
        if (DataSaver.instance.useCameraBackground)
        {
            DataSaver.instance.useCameraBackground = false;
            arOnOff.text = "OFF";
            return;
        }

        DataSaver.instance.useCameraBackground = true;
        arOnOff.text = "ON";
    }

    public void Voicecontrol()
    {
        if (DataSaver.instance.voicecontrol)
        {
            DataSaver.instance.voicecontrol = false;
            voicecontrolOnOff.text = "OFF";
            return;
        }

        DataSaver.instance.voicecontrol = true;
        voicecontrolOnOff.text = "ON";
    }

    public void Tutorial1()
    {
        /*if (tutorialOpen)
        {
            tutorialMenu.SetActive(false);
            tutorialOpen = false;
            return;
        }

        tutorialMenu.SetActive(true);
        tutorialOpen = true;*/

        SceneManager.LoadScene(5);
    }

    public void Tutorial2()
    {
        SceneManager.LoadScene(6);
    }

    public void ToStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToPreviousTutorial()
    {
        SceneManager.LoadScene(5);
    }
}