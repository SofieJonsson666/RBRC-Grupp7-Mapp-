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
    private float CBSizeDifference = 1.93f;
    private int sizeOffset = 70;

    private void Start()
    {
        phoneScript.Activate();
        size = (Screen.height * 3) / 5 + sizeOffset;
        print(size);
        //float frameSize = size / (Camera.main.orthographicSize * 2f) / 10;
        //frame.transform.localScale = new Vector3(frameSize, frameSize);
        //birdPreview.transform.localScale = new Vector3(frameSize * CBSizeDifference, frameSize * CBSizeDifference);
        //print(frameSize);
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
        int XSlut = Screen.width / 2 + size / 2;
        int YSlut = Screen.height / 2 + size / 2;

        photo.ReadPixels(new Rect(XStart, YStart, XSlut, YSlut), 0, 0);
        Debug.Log("sw: " +XStart  + " SH: " + YStart + " SIZE:"+ size + "sw: " + XSlut + " SH: " + YSlut);
        photo.Apply();

        InvertActives();

        Sprite sprite = Sprite.Create(photo, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f));
        birdSprite.sprite = sprite;
       // birdSprite.transform.localScale = new Vector3(size, size);
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
