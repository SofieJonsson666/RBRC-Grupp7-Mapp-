using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdCamera : MonoBehaviour
{
    [SerializeField] private int size;
    [SerializeField] private GameObject pictureBtns;
    [SerializeField] private GameObject cameraBtn;
    [SerializeField] private GameObject birdPreview;
    [SerializeField] private GameObject frame;
    [SerializeField] private SpriteRenderer birdSprite;
    [SerializeField] private PhoneCamera phoneScript;

    private void Start()
    {
        phoneScript.Activate();
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

        int SW = Screen.width / 2 - size / 2;
        int SH = Screen.height / 2 - size / 2;

        photo.ReadPixels(new Rect(SW, SH, size, size), 0, 0);
        photo.Apply();

        InvertActives();

        Sprite sprite = Sprite.Create(photo, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f));
        birdSprite.sprite = sprite;
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
