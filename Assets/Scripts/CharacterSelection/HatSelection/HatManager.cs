using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatManager : MonoBehaviour
{

    public HatDatabase hatDB;

    public Image hatSR;
    public Image hatIconSR;
    public Image hatIconLSR;
    public Image hatIconRSR;

    private int selectionHatOption = 1;
    private int selectionToLeft = 0;
    private int selectionToRight = 2;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectionHatOption"))
        {
            selectionHatOption = 1;
            selectionToLeft = 0;
            selectionToRight = 2;
        }
        else
        {
            //Load();
        }

        UpdateHat(selectionHatOption);
        UpdateLeftHat(selectionToLeft);
        UpdateRightHat(selectionToRight);
    }

    public void NextOption()
    {
        selectionHatOption++;
        selectionToLeft++;
        selectionToRight++;

        if (selectionHatOption >= hatDB.HatCount)
        {
            selectionHatOption = 0;
        }
        if (selectionToLeft >= hatDB.HatCount)
        {
            selectionToLeft = 0;
        }
        if (selectionToRight >= hatDB.HatCount)
        {
            selectionToRight = 0;
        }

        UpdateHat(selectionHatOption);
        UpdateLeftHat(selectionToLeft);
        UpdateRightHat(selectionToRight);
        Save();
    }

    public void BackOption()
    {
        selectionHatOption--;
        selectionToLeft--;
        selectionToRight--;

        if (selectionHatOption < 0)
        {
            selectionHatOption = hatDB.HatCount - 1;
        }
        if (selectionToLeft < 0)
        {
            selectionToLeft = hatDB.HatCount - 1;
        }
        if (selectionToRight < 0)
        {
            selectionToRight = hatDB.HatCount - 1;
        }

        UpdateHat(selectionHatOption);
        UpdateLeftHat(selectionToLeft);
        UpdateRightHat(selectionToRight);
        Save();
    }

    private void UpdateHat(int selectedOption)
    {
        Hat hat = hatDB.GetHat(selectedOption);
        hatIconSR.sprite = hat.hatSpriteIcon;
        hatSR.sprite = hat.hatSprite;
    }

    private void UpdateLeftHat(int selectLeft)
    {
        Hat hat = hatDB.GetHat(selectLeft);
        hatIconLSR.sprite = hat.hatSpriteIcon;

    }

    private void UpdateRightHat(int selectRight)
    {
        Hat hat = hatDB.GetHat(selectRight);
        hatIconRSR.sprite = hat.hatSpriteIcon;

    }
    public void Save()
    {
        PlayerPrefs.SetInt("selectionHatOption", selectionHatOption);
    }

    /*public void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectionOption);
        PlayerPrefs.SetInt("selectionToLeft", selectionToLeft);
        PlayerPrefs.SetInt("selectionToRight", selectionToRight);
    }

    private void Load()
    {
        selectionOption = PlayerPrefs.GetInt("selectedOption");
        selectionOption = PlayerPrefs.GetInt("selectionToLeft");
        selectionOption = PlayerPrefs.GetInt("selectionToRight");
    }*/
}
