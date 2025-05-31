using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static SaveSeedData;
using UnityEngine.SceneManagement;

public class DataSaver : MonoBehaviour
{
    public static DataSaver instance;

    public int highScore = 0;
    public int totalSeedAmount;
    public int seedAmount;
    public bool useCameraBackground;
    public bool gyro;
    public bool voicecontrol;

    public Sprite CBSprite;

    public float mps;
    public float metersTraveled;
    public float totalMetersTraveled;

    public string selectedLanguage = "en";

    public List<int> unlockedCharacterIndices = new List<int>();

    private string savePath;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "seedsave.json");
        LoadGame(); //kommer ihåg saker från andra spelsessioner när spelet börjar -sofie
    }

    public void SaveGame()
    {
        SeedSaveData saveData = new SeedSaveData
        {
            highScore = this.highScore,
            totalSeedAmount = this.totalSeedAmount,
            totalMetersTraveled = this.totalMetersTraveled,
            selectedLanguage = this.selectedLanguage,
            useCameraBackground = this.useCameraBackground,
            gyro = this.gyro,
            voicecontrol = this.voicecontrol,

            unlockedCharacterIndices = new List<int>(this.unlockedCharacterIndices)
        };

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved to: " + savePath);

        if (string.IsNullOrEmpty(savePath))
        {
            savePath = Path.Combine(Application.persistentDataPath, "seedsave.json");
        }
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SeedSaveData saveData = JsonUtility.FromJson<SeedSaveData>(json);

            this.highScore = saveData.highScore;
            this.totalSeedAmount = saveData.totalSeedAmount;
            this.totalMetersTraveled = saveData.totalMetersTraveled;
            this.selectedLanguage = saveData.selectedLanguage;
            this.useCameraBackground = saveData.useCameraBackground;
            this.gyro = saveData.gyro;
            this.voicecontrol = saveData.voicecontrol;
            this.unlockedCharacterIndices = saveData.unlockedCharacterIndices ?? new List<int>();
        }
        else
        {
            Debug.Log("No save file found. Using defaults.");
            InitializeDefaults();
            SaveGame(); 
        }

        if (!unlockedCharacterIndices.Contains(0))
        {
            unlockedCharacterIndices.Add(0); 
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame(); 
    }

    public void ResetGameData()
    {
        /*highScore = 0;
        totalSeedAmount = 0;
        seedAmount = 0;
        totalMetersTraveled = 0;
        unlockedCharacterIndices.Clear();
        selectedLanguage = "en";


        SaveGame();
        Debug.Log("Game data reset!");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //this reloads gamedata used for debugging and editing, remove when game is finished*/


        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        InitializeDefaults();
        SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void InitializeDefaults()
    {
        highScore = 0;
        totalSeedAmount = 0;
        seedAmount = 0;
        totalMetersTraveled = 0;
        unlockedCharacterIndices.Clear();
        selectedLanguage = "en";
        unlockedCharacterIndices.Add(0); 
    }

    public void UpdateCBSprite(Sprite sprite)
    {
        CBSprite = sprite;
    }

    public void ApplyCBSprite(SpriteRenderer CBRenderer)
    {
        if (CBSprite != null)
        {
            CBRenderer.sprite = CBSprite;
        }
    }
}
