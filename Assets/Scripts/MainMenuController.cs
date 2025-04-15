using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void SceneLoader()
    {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        print(DataSaver.instance.seedAmount);
    }
}
