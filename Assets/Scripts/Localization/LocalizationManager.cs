using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizationManager : MonoBehaviour
{

    public static LocalizationManager Instance;

    private Dictionary<string, string> localizedText;
    public string currentLanguage = "en";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //LoadLocalizedText(currentLanguage);

            if (DataSaver.instance != null && !string.IsNullOrEmpty(DataSaver.instance.selectedLanguage))
            {
                currentLanguage = DataSaver.instance.selectedLanguage;
            }
            LoadLocalizedText(currentLanguage);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLocalizedText(string languageCode)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>($"Localization/{languageCode}");
        if (jsonFile == null)
        {
            Debug.LogError("Localization file not found: " + languageCode);
            return;
        }

        localizedText = JsonUtility.FromJson<LocalizationData>(jsonFile.text).ToDictionary();
        currentLanguage = languageCode;

        if (DataSaver.instance != null)
        {
            DataSaver.instance.selectedLanguage = languageCode;
        }

        Debug.Log($"Loaded JSON for {languageCode}: {jsonFile.text}");
        UpdateAllTexts();
    }

    public string GetLocalizedValue(string key)
    {
        if (localizedText != null && localizedText.ContainsKey(key))
            return localizedText[key];

        return $"[{key}]";
    }

    public void UpdateAllTexts()
    {
        foreach (LocalizedText lt in FindObjectsOfType<LocalizedText>())
        {
            lt.UpdateText();
        }
    }
}

[System.Serializable]
public class LocalizationData
{
    public List<LocalizedItem> items;

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        foreach (var item in items)
        {
            dict[item.key] = item.value;
        }
        return dict;
    }
}

[System.Serializable]
public class LocalizedItem
{
    public string key;
    public string value;
}