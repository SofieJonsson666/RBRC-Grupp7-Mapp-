using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageSelector : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;

    private void Start()
    {

        StartCoroutine(InitializeDropdown());
    }

    IEnumerator InitializeDropdown()
    {
        while (LocalizationManager.Instance == null)
            yield return null;

        int savedIndex = PlayerPrefs.GetInt("languageIndex", 0);

        languageDropdown.SetValueWithoutNotify(savedIndex);

        OnLanguageDropdownChanged(savedIndex);
    }


    public void SetLanguageEnglish()
    {
        LocalizationManager.Instance.LoadLocalizedText("en");
        DataSaver.instance.selectedLanguage = "en";
    }

    public void SetLanguageSwedish()
    {
        Debug.Log("Switching to Swedish");
        LocalizationManager.Instance.LoadLocalizedText("sv");
        DataSaver.instance.selectedLanguage = "sv";
    }

    public void SetLanguageJapanese()
    {
        LocalizationManager.Instance.LoadLocalizedText("jp");
        DataSaver.instance.selectedLanguage = "jp";
    }

    public void OnLanguageDropdownChanged(int index)
    {
        Debug.Log("Dropdown changed to index: " + index);

        PlayerPrefs.SetInt("languageIndex", index);
        PlayerPrefs.Save();

        switch (index)
        {
            case 0:
                SetLanguageEnglish();
                DataSaver.instance.selectedLanguage = "en";
                break;
            case 1:
                SetLanguageSwedish();
                DataSaver.instance.selectedLanguage = "sv";
                break;
            case 2:
                SetLanguageJapanese();
                DataSaver.instance.selectedLanguage = "jp";
                break;
        }

        LocalizationManager.Instance?.UpdateAllTexts();

    }


}
