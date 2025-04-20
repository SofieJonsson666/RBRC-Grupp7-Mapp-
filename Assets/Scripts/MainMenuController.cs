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
    [SerializeField] private TMP_Text gyroOnOff;

    private void Start()
    {
        if (DataSaver.instance.ar) arOnOff.text = "ON";
        else arOnOff.text = "OFF";

        if (DataSaver.instance.gyro) gyroOnOff.text = "ON";
        else gyroOnOff.text = "OFF";
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

    public void Gyro()
    {
        if (DataSaver.instance.gyro)
        {
            DataSaver.instance.gyro = false;
            gyroOnOff.text = "OFF";
            return;
        }

        DataSaver.instance.gyro = true;
        gyroOnOff.text = "ON";
    }
}
