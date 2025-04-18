using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public static DataSaver instance;

    public int seedAmount;
    public bool ar;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        ar = false;
    }

    public void UpdateSeedAmount(int change)
    {
        seedAmount = seedAmount + change;
    }
}
