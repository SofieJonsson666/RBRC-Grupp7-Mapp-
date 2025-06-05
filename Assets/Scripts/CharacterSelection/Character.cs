using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]

public class Character
{

    public string characterName;
    public Sprite characterSprite;

    public GameObject characterPrefab;

    public AnimatorOverrideController overrideController;
    public Image hatOnBird;
    public RectTransform birdSize;


    public int seedCost = 0;
    public bool isUnlocked = false; //DO NOT TOGGLE THIS IN INSPECTOR!!! Vi g�r det h�r i koden i save data -sofie
}
