using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdCamera : MonoBehaviour
{
    [SerializeField] private GameObject pictureBtns;
    [SerializeField] private GameObject cameraBtn;
    [SerializeField] private GameObject birdPreview;
    [SerializeField] private GameObject frame;
    [SerializeField] private SpriteRenderer birdSprite;
    [SerializeField] private PhoneCamera phoneScript;

    private int size;

    private void Start()
    {
        if (!phoneScript.Activate())
        {
            SceneManager.LoadScene(1);
        }
        size = (Screen.height * 3) / 5;
        print(size);
    }

    public void TakePictureAndApply()
    {
        StartCoroutine(CapturePhoto());
    }

    private IEnumerator CapturePhoto()
    {
        frame.SetActive(false);
        cameraBtn.SetActive(false);

        yield return null;
        yield return new WaitForEndOfFrame();

        Texture2D photo = new Texture2D(size, size, TextureFormat.RGB24, false);

        int XStart = Screen.width / 2 - size / 2;
        int YStart = Screen.height / 2 - size / 2;
        int XEnd = Screen.width / 2 + size / 2;
        int YEnd = Screen.height / 2 + size / 2;

        photo.ReadPixels(new Rect(XStart, YStart, size, size), 0, 0);
        Debug.Log("sw: " + XStart + " SH: " + YStart + " SIZE:" + size + "sw: " + XEnd + " SH: " + YEnd);
        photo.Apply();

        InvertActives();

        Sprite sprite = Sprite.Create(photo, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f));
        birdSprite.sprite = sprite;

        birdSprite.transform.localScale = new Vector3(0.4f, 0.4f);
        DataSaver.instance.UpdateCBSprite(sprite);
    }

    public void InvertActives()
    {
        if (birdPreview.activeSelf)
        {
            cameraBtn.SetActive(true);
            frame.SetActive(true);
            birdPreview.SetActive(false);
            pictureBtns.SetActive(false);
            return;
        }
        birdPreview.SetActive(true);
        pictureBtns.SetActive(true);
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
    }
}
