using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSeedData : MonoBehaviour
{
    [System.Serializable]
    public class SeedSaveData
    {
        public int highScore;
        public int totalSeedAmount;
        public float totalMetersTraveled;

        public string selectedLanguage;
        public bool ar;
        public bool gyro;
        public bool voicecontrol;

        public List<int> unlockedCharacterIndices = new List<int>();
    }
}
