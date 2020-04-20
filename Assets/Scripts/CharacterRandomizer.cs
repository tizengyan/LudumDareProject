using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterRandomizer : MonoBehaviour
{
    public int maxCharacters;
    [SerializeField] private int activeCharacter = -1;

    public Image image;
    public Sprite[] characterTexture;
    public UI_SwipeController swipeController;
    public UI_MainMenuController UIController;


    public void ShowTutorialMenu() {
        swipeController.ShowTutorial();
    }

    public void ChangeCharacter()
    {
        Debug.Log("Finished Character: " + DataManager.FinishedCharacters);

        if (DataManager.FinishedCharacters == maxCharacters)
        {
            UIController.GameWin();

            CleanGameData();

            return;
        }

        image = gameObject.GetComponent<Image>();

        activeCharacter++;

        if (activeCharacter > maxCharacters - 1)
        {
            activeCharacter = 0;
        }

        if (DataManager.FinishedCharacters != maxCharacters)
        {
            while (DataManager.CharacterIsSelected[activeCharacter])
            {
                activeCharacter++;

                if (activeCharacter > maxCharacters - 1)
                {
                    activeCharacter = 0;
                }
            }
        }

        image.sprite = characterTexture[activeCharacter];
    }

    public void CharacterSelect()
    {
        DataManager.CharacterIsSelected[activeCharacter] = true;

        DataManager.AddCharacter();
    }

    public void CleanGameData()
    {
        activeCharacter = -1;

        DataManager.ClearCharacterData();
    }
}
