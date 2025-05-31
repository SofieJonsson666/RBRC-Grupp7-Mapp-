using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHat : MonoBehaviour
{

    public HatDatabase hatDB;

    public SpriteRenderer spriteRenderer;

    private int selectedHatOption = 1;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectionHatOption"))
        {
            selectedHatOption = 1;
        }
        else
        {
            Load();
        }

        UpdateCharacter(selectedHatOption);
    }

    private void UpdateCharacter(int selectedOption)
    {
        Hat hat = hatDB.GetHat(selectedOption);
        spriteRenderer.sprite = hat.hatSprite;
    }

    private void Load()
    {
        selectedHatOption = PlayerPrefs.GetInt("selectionHatOption");
    }
}
