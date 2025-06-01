using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string key;
    public TMP_FontAsset fontEnglish;
    public TMP_FontAsset fontSwedish;
    public TMP_FontAsset fontJapanese;

    private TMP_Text textField;

    private void Start()
    {
        textField = GetComponent<TMP_Text>();
        UpdateText();
    }

    public void UpdateText()
    {
        try
        {
            if (textField == null)
                textField = GetComponent<TMP_Text>();

            string translation = LocalizationManager.Instance.GetLocalizedValue(key);
            Debug.Log($"Localized key: {key} => {translation}");

            textField.text = translation;

            switch (LocalizationManager.Instance.currentLanguage)
            {
                case "en":
                    textField.font = fontEnglish;
                    break;

                case "sv":
                    textField.font = fontSwedish;
                    break;

                case "jp":
                    textField.font = fontJapanese;
                    break;
            }

            Debug.Log($"[{gameObject.name}] Updating key '{key}' to '{LocalizationManager.Instance.GetLocalizedValue(key)}'");
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}