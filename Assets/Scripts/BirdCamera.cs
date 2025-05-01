using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BirdCamera : MonoBehaviour
{
    public void TakePhoto()
    {
        StartCoroutine(TakeThePhoto());
    }

    IEnumerator TakeThePhoto()
    {
        yield return new WaitForEndOfFrame();
        Camera camera = Camera.main;
        int width = Screen.width;
        int height = Screen.height;
        RenderTexture rt = new RenderTexture(width, height, 24);
        camera.targetTexture = rt;

        var currentRT = RenderTexture.active;
        RenderTexture.active = rt;

        camera.Render();
        int centerW = width / 2;
        int centerH = height / 2;
        const int size = 300;
        Texture2D image = new Texture2D(size, size);

        image.ReadPixels(new Rect(centerW - size/2, centerH - size/2, size, size), 0, 0);
        image.Apply();

        camera.targetTexture = null;

        RenderTexture.active = currentRT;

        byte[] bytes = image.EncodeToPNG();
        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, bytes);

        Destroy(rt);
        Destroy(image);
    }
}
