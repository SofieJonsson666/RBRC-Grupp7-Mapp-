using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{

    public CharacterDatabase characterDB;

    public TMP_Text nameText;
    public SpriteRenderer spriteRenderer;
    public Image lockIconBird;

    private int selectedOption = 0;

    // Start is called before the first frame update
    void Start()
    {
        SyncUnlocks();

        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }

        UpdateCharacter(selectedOption);
    }

    private void SyncUnlocks()
    {
        for (int i = 0; i < characterDB.CharacterCount; i++)
        {
            Character character = characterDB.GetCharacter(i);
            //character.isUnlocked = DataSaver.instance.unlockedCharacterIndices.Contains(i) || character.seedCost == 0;
            if (i == 0)
            {
                character.isUnlocked = true;
                if (!DataSaver.instance.unlockedCharacterIndices.Contains(i))
                    DataSaver.instance.unlockedCharacterIndices.Add(i);
            } 
            else
            {
                character.isUnlocked = DataSaver.instance.unlockedCharacterIndices.Contains(i);
            }
        }
    }

    public void UnlockCharacter()
    {
        Character character = characterDB.GetCharacter(selectedOption);

        if (!character.isUnlocked && DataSaver.instance.totalSeedAmount >= character.seedCost)
        {
            DataSaver.instance.totalSeedAmount -= character.seedCost;
            character.isUnlocked = true;

            // Sparar info om vilka karaktärer är unlocked
            if (!DataSaver.instance.unlockedCharacterIndices.Contains(selectedOption))
            {
                DataSaver.instance.unlockedCharacterIndices.Add(selectedOption);
            }

            DataSaver.instance.SaveGame();
            UpdateCharacter(selectedOption);

            Debug.Log($"{character.characterName} unlocked!");
        }
        else
        {
            Debug.Log("Not enough seeds to unlock this character! U poor");
        }
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= characterDB.CharacterCount)
        {
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    public void BackOption()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = characterDB.CharacterCount - 1;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        spriteRenderer.sprite = character.characterSprite;
        //nameText.text = character.characterName;

        if (character.isUnlocked)
        {
            nameText.text = character.characterName + " (Unlocked)";
        }
        else
        {
            nameText.text = character.characterName + $" (Locked: {character.seedCost} seeds)";
        }
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void ChangeScene(int sceneID)
    {
        Save();
        SceneManager.LoadScene(sceneID);
    }
}
