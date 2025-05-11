using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickPhoto : MonoBehaviour
{
    public UnityEvent FunctionOnPickedReturn;
    public UnityEvent FunctionOnSavedReturn;

    public NativeFilePicker.Permission permission;

    /*public void SavePhotoToCameraRoll(Texture2D MyTexture, string AlbumName, string fileName)
    {
        NativeGallery.SaveImageToGallery(MyTexture, AlbumName, fileName, (callback, path) =>
          {

          })
    }*/
}
