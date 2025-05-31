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
    [SerializeField] private GameObject cameraBackground;

    public bool Activate()
    {
        if (cam != null && cam.isPlaying)
        {
            cam.Stop();
            cam = null;
        }

        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            cameraBackground.SetActive(false);
            print("Fel");
            camAvailable = false;
            return false;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing && backSelected)
            {
                cam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
            else if (devices[i].isFrontFacing && !backSelected)
            {
                cam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if (cam == null)
        {
            print("ingen kamera");

            if (!backSelected)
            {
                SceneManager.LoadScene(5);
            }
            return false;
        }

        defaultBackground = background.texture;
        cameraBackground.SetActive(true);

        cam.Play();
        background.texture = cam;

        camAvailable = true;

        return true;
    }

    private void Update()
    {
        if (!camAvailable)
        {
            return;
        }

        float ratio = (float)cam.width / (float)cam.height;
        fit.aspectRatio = ratio;

        float scaleY = cam.videoVerticallyMirrored ? -1 : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -cam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
}
