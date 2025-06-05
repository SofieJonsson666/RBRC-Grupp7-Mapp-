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
    //public SpriteRenderer spriteRenderer;
    public Image spriteRenderer;
    public Image lockIconBird;
    public Image hatPosition;
    public TMP_Text unlockHintText;
    public RectTransform thisTransform;

    private int selectedOption = 0;

    public Material normalMaterial;
    public Material greyMaterial;

    [SerializeField] private GameObject customBird;

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
        SetHatPosition(hatPosition);
    }

    private void SyncUnlocks()
    {
        for (int i = 0; i < characterDB.CharacterCount; i++)
        {
            Character character = characterDB.GetCharacter(i);

            if (i == 0)
            {
                character.isUnlocked = true;
                if (!DataSaver.instance.unlockedCharacterIndices.Contains(i))
                    DataSaver.instance.unlockedCharacterIndices.Add(i);
            }
            else if (DataSaver.instance.unlockedCharacterIndices.Contains(i))
            {
                character.isUnlocked = true;
            }
            else if (DataSaver.instance.totalSeedAmount >= character.seedCost)
            {
                character.isUnlocked = true;
                DataSaver.instance.unlockedCharacterIndices.Add(i);
            }
            else
            {
                character.isUnlocked = false;
            }
        }

        DataSaver.instance.SaveGame();
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
        SetHatPosition(hatPosition);
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
        SetHatPosition(hatPosition);
        Save();
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        spriteRenderer.sprite = character.characterSprite;
        customBird.SetActive(false);
        //nameText.text = character.characterName;

        if (selectedOption == 2)
        {
            customBird.SetActive(true);
            if (DataSaver.instance.CBSprite != null)
            {      
                DataSaver.instance.ApplyCBSprite(customBird.GetComponent<SpriteRenderer>());
            }        
            print("CB");
        }

        if (!character.isUnlocked)
        {
            /*int needed = character.seedCost - DataSaver.instance.totalSeedAmount;
            needed = Mathf.Max(0, needed);
            unlockHintText.text = $"Need {needed} more seeds to unlock.";*/

            nameText.text = character.characterName;
            lockIconBird.gameObject.SetActive(true);
            spriteRenderer.material = greyMaterial;

            int needed = character.seedCost - DataSaver.instance.totalSeedAmount;
            unlockHintText.text = $"Need {needed} more seeds to unlock";
        }
        else
        {
            nameText.text = character.characterName;
            lockIconBird.gameObject.SetActive(false);
            spriteRenderer.material = normalMaterial;
            unlockHintText.text = "";
        }
    }

    private void SetHatPosition(Image hatOnBird)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        hatOnBird.rectTransform.anchoredPosition = character.hatOnBird.rectTransform.anchoredPosition;
        hatOnBird.rectTransform.rotation = character.hatOnBird.rectTransform.rotation;
        thisTransform.sizeDelta = character.birdSize.sizeDelta;
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
