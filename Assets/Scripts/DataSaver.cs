using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public static DataSaver instance;

    public int highScore = 0;
    public int totalSeedAmount;
    public int seedAmount;
    public bool ar;
    public bool gyro;

    public float mps;
    public float metersTraveled;
    public float totalMetersTraveled;

    public string selectedLanguage = "en";

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}