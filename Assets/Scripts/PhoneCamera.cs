using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;
    //public Renderer backgroundRenderer;
    [SerializeField] private GameObject cameraBackground;

    [SerializeField] private GameObject backgroundDetector;
    [SerializeField] private GameObject backgroundSpawner;
    [SerializeField] private GameObject backgroundController;

    private void Start()
    {
        if (!DataSaver.instance.ar)
        {
            cameraBackground.SetActive(false);
            return;
        }

        cameraBackground.SetActive(true);
        backgroundDetector.SetActive(false);
        backgroundSpawner.SetActive(false);
        backgroundController.SetActive(false);

        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            print("Fel");
            camAvailable = false;
            return;
        }

        for(int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if(backCam == null)
        {
            print("ingen bakamera");
            return;
        }

        backCam.Play();
        background.texture = backCam;
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

         float ratio = (float)backCam.width / (float)backCam.height;
         fit.aspectRatio = ratio;

         float scaleY = backCam.videoVerticallyMirrored ? -1 : 1f;
         background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

         int orient = -backCam.videoRotationAngle;
         background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
}
