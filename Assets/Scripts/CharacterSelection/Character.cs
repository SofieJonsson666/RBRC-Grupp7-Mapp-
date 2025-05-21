using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class Character
{

    public string characterName;
    public Sprite characterSprite;
    public AnimatorOverrideController overrideController;

    
    public int seedCost = 0;
    public bool isUnlocked = false; //DO NOT TOGGLE THIS IN INSPECTOR!!! Vi g�r det h�r i koden i save data -sofie
}
