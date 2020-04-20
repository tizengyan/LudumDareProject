using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterRandomizer : MonoBehaviour
{
    public int maxCharacters;
    [SerializeField] private int activeCharacter = -1;
    private int finishedCharacters = 0;

    public Image image;
    public Sprite[] characterTexture;
    public bool[] characterIsSelected;
    public UI_SwipeController swipeController;
    public UI_MainMenuController UIController;

    public void ShowTutorialMenu() {
        swipeController.ShowTutorial();
    }

    public void ChangeCharacter()
    {
        if (finishedCharacters == maxCharacters)
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

        if (finishedCharacters != maxCharacters)
        {
            while (characterIsSelected[activeCharacter])
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
        characterIsSelected[activeCharacter] = true;

        finishedCharacters++;
    }

    public void CleanGameData()
    {
        activeCharacter = -1;
        finishedCharacters = 0;

        for (int i = 0; i < maxCharacters; i++)
        {
            characterIsSelected[i] = false;
        }
    }
}
