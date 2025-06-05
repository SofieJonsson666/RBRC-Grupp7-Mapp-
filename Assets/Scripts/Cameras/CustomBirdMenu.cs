using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomBirdMenu : MonoBehaviour
{
    [SerializeField] private Camera camera;

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void FlipCamera()
    {
        PhoneCamera cameraScript = camera.GetComponent<PhoneCamera>();
        cameraScript.backSelected = cameraScript.backSelected ? false : true;
        cameraScript.Activate();
    }
}
