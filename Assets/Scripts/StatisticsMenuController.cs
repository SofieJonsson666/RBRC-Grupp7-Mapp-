using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatisticsMenuController : MonoBehaviour
{
    public void BackToMainMenu()
    {
        Debug.Log("HELLO WE SHOULD GO BACK NOW");
        SceneManager.LoadScene(0);
    }
}