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
    public bool ar;
    public bool gyro;
    public bool voicecontrol;

    //Custom Bird saker
    public Sprite CBSprite;
    public SpriteRenderer CBRenderer;

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

        savePath = Application.persistentDataPath + "/seedsave.json";
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
            ar = this.ar,
            gyro = this.gyro,
            voicecontrol = this.voicecontrol,

            unlockedCharacterIndices = new List<int>(this.unlockedCharacterIndices)
        };

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved to: " + savePath);
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
            this.ar = saveData.ar;
            this.gyro = saveData.gyro;
            this.voicecontrol = saveData.voicecontrol;

            this.unlockedCharacterIndices = saveData.unlockedCharacterIndices ?? new List<int>();

            Debug.Log("Game loaded from: " + savePath);
        }
        else
        {
            Debug.Log("No save file! Starting with default values!");
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame(); // this autosaves
    }

    public void ResetGameData()
    {
        highScore = 0;
        totalSeedAmount = 0;
        seedAmount = 0;
        totalMetersTraveled = 0;
        unlockedCharacterIndices.Clear();
        selectedLanguage = "en";
        ar = false;
        gyro = false;
        voicecontrol = false;


        SaveGame();
        Debug.Log("Game data reset!");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //this reloads gamedata used for debugging and editing, remove when game is finished
    }

    public void UpdateCBSprite(Sprite sprite)
    {
        CBSprite = sprite;
    }

    public void ApplyCBSprite()
    {
        if(CBSprite != null)
        {
            CBRenderer.sprite = CBSprite;
        }
    }
}
