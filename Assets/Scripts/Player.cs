using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    public CharacterDatabase characterDB;
 
    public GameObject customBird;

    public SpriteRenderer spriteRenderer;

    public Animator animator;

    [SerializeField] private BirdControllerVer3 birdController;

    private int selectedOption = 0;

    void Start()
    {
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

    private void UpdateCharacter(int selectedOption)
    {
        if (selectedOption == 2)
        {
            birdController.CBSelected = true;
            customBird.SetActive(true);
            DataSaver.instance.ApplyCBSprite(customBird.GetComponent<SpriteRenderer>());

            spriteRenderer.sprite = null;
            Destroy(spriteRenderer);

            if (animator != null)
            {
                animator.runtimeAnimatorController = null;
            }
        }

        else
        {
            customBird.SetActive(false);
            Character character = characterDB.GetCharacter(selectedOption);
            spriteRenderer.sprite = character.characterSprite;

            if (animator != null && character.overrideController != null)
            {
                animator.runtimeAnimatorController = character.overrideController;
            }
        }
        Debug.Log("Loaded character index: " + selectedOption);
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

}
