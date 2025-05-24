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
    public bool gameScene;
    //public Renderer backgroundRenderer;
    [SerializeField] private GameObject cameraBackground;

    [SerializeField] private GameObject backgroundDetector;
    [SerializeField] private GameObject backgroundSpawner;
    [SerializeField] private GameObject backgroundController;
    [SerializeField] private GameObject house;

    [SerializeField] private int size;
    //public Renderer targetRenderer;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private GameObject pictureBtns;
    [SerializeField] private GameObject cameraBtn;
    [SerializeField] private GameObject picturePreview;
    [SerializeField] private GameObject frame;
    [SerializeField] private SpriteRenderer bird;

    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        if (cam != null && cam.isPlaying)
        {
            cam.Stop();
            cam = null;
        }

        if (backSelected && gameScene)
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
            return;
        }

        defaultBackground = background.texture;
        cameraBackground.SetActive(true);

        if (gameScene)
        {
            backgroundDetector.SetActive(false);
            backgroundSpawner.SetActive(false);
            backgroundController.SetActive(false);
            house.SetActive(false);
        }

        cam.Play();
        background.texture = cam;

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

 /*   public Texture2D CapturePhoto()
    {
        Texture2D photo = new Texture2D(size, size);
        //photo.SetPixels(cam.GetPixels());
        photo.ReadPixels(new Rect(Screen.width - size / 2, Screen.height - size / 2, size, size), 0, 0);
        photo.Apply();
        return photo;
    }*/

    public void TakePictureAndApply()
    {
        StartCoroutine(CapturePhoto());
      /*  Texture2D capturedImage = CapturePhoto();
        InvertActives();
        if (capturedImage != null)
        {      
            targetRenderer.material.mainTexture = capturedImage;
        }*/
    }

    private IEnumerator CapturePhoto()
    {
        frame.SetActive(false);
        cameraBtn.SetActive(false);

        yield return null;
        yield return new WaitForEndOfFrame();

        Texture2D photo = new Texture2D(size, size, TextureFormat.RGB24, false);

        int SW = Screen.width / 2 - size / 2;
        int SH = Screen.height / 2 - size / 2;   

        photo.ReadPixels(new Rect(SW, SH, size, size), 0, 0);
        photo.Apply();

        InvertActives();

        if (rawImage != null)
        {
            //rawImage.material.mainTexture = photo;
            rawImage.texture = photo;

            Material photoMaterial = new Material(Shader.Find("Standard"));
            photoMaterial.mainTexture = photo;
            //plane.GetComponent<Renderer>().material = photoMaterial;

            Sprite sprite = Sprite.Create(photo, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f));
            bird.sprite = sprite;

            bird.enabled = false;
            bird.enabled = true;
        } 
    }

    public void InvertActives()
    {
        if (picturePreview.activeSelf)
        {
            cameraBtn.SetActive(true);
            frame.SetActive(true);
            picturePreview.SetActive(false); 
            pictureBtns.SetActive(false);
            return;
        }
        picturePreview.SetActive(true);
        pictureBtns.SetActive(true);
    }

    public void Continue()
    {

    }
}
