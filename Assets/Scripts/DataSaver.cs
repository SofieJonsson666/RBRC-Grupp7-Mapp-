using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public static DataSaver instance;

    public int seedAmount;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateSeedAmount(int change)
    {
        seedAmount = seedAmount + change;
    }
}
