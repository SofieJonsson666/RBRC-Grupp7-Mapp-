using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageSelector : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;

    public TMP_FontAsset fontEnglish;
    public TMP_FontAsset fontJapanese;
    public TMP_FontAsset fontSwedish;

    public TMP_Text dropdownLabel;
    public TMP_Text dropdownItemText;

    private void Start()
    {

        if (dropdownLabel == null)
            dropdownLabel = languageDropdown.captionText;

        if (dropdownItemText == null)
            dropdownItemText = languageDropdown.itemText;

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

        if (dropdownLabel != null) dropdownLabel.font = fontEnglish;
        if (dropdownItemText != null) dropdownItemText.font = fontEnglish;

        UpdateDropdownFonts(fontEnglish);
    }

    public void SetLanguageSwedish()
    {
        Debug.Log("Switching to Swedish");
        LocalizationManager.Instance.LoadLocalizedText("sv");
        DataSaver.instance.selectedLanguage = "sv";

        if (dropdownLabel != null) dropdownLabel.font = fontSwedish;
        if (dropdownItemText != null) dropdownItemText.font = fontSwedish;

        UpdateDropdownFonts(fontSwedish);
    }

    public void SetLanguageJapanese()
    {
        LocalizationManager.Instance.LoadLocalizedText("jp");
        DataSaver.instance.selectedLanguage = "jp";

        if (dropdownLabel != null) dropdownLabel.font = fontJapanese;
        if (dropdownItemText != null) dropdownItemText.font = fontJapanese;

        UpdateDropdownFonts(fontJapanese);
    }

    private void UpdateDropdownFonts(TMP_FontAsset targetFont)
    {
        TMP_Dropdown.DropdownEvent dropdownEvent = languageDropdown.onValueChanged;
        languageDropdown.RefreshShownValue();

        TMP_Text[] texts = languageDropdown.template.GetComponentsInChildren<TMP_Text>(true);
        foreach (var text in texts)
        {
            text.font = targetFont;
        }
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
