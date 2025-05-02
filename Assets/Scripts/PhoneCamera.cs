using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhoneCamera : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture cam;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;
    public bool backSelected;
    //public Renderer backgroundRenderer;
    [SerializeField] private GameObject cameraBackground;

    [SerializeField] private GameObject backgroundDetector;
    [SerializeField] private GameObject backgroundSpawner;
    [SerializeField] private GameObject backgroundController;

    private void Start()
    {
        if (backSelected)
        {
            if (!DataSaver.instance.ar)
            {
                cameraBackground.SetActive(false);
                camAvailable = false;
                return;
            }
        }  

        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            cameraBackground.SetActive(false);
            print("Fel");
            camAvailable = false;
            return;
        }

        for(int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing && backSelected)
            {
                cam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
            else if(devices[i].isFrontFacing && !backSelected)
            {
                cam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if(cam == null)
        {
            print("ingen kamera");

            if (!backSelected)
            {
                SceneManager.LoadScene(5);
            }
            return;
        }

        defaultBackground = background.texture;

        if (backSelected)
        {
            cameraBackground.SetActive(true);
            backgroundDetector.SetActive(false);
            backgroundSpawner.SetActive(false);
            backgroundController.SetActive(false);
        }

        cam.Play();
        background.texture = cam;
        //backgroundRenderer.material.mainTexture = backCam;

        camAvailable = true;
    }

    private void Update()
    {
        if (!camAvailable)
        {
            return;
        }

        /*Vector3 scale = backgroundRenderer.transform.localScale;
        scale.y = backCam.videoVerticallyMirrored ? -Mathf.Abs(scale.y) : Mathf.Abs(scale.y);
        backgroundRenderer.transform.localScale = scale;

        backgroundRenderer.transform.localEulerAngles = new Vector3(0, 0, -backCam.videoRotationAngle);*/

         float ratio = (float)cam.width / (float)cam.height;
         fit.aspectRatio = ratio;

         float scaleY = cam.videoVerticallyMirrored ? -1 : 1f;
         background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

         int orient = -cam.videoRotationAngle;
         background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
}
