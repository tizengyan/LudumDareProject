using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterRandomizer : MonoBehaviour
{
    public Image image;
    public Sprite[] characterTexture;
    public UI_SwipeController swipeController;

    public void ChangeCharacter()
    {
        image = gameObject.GetComponent<Image>();

        int i = GetRandomIndex();

        while (i == swipeController.previousCharacterIndex)
        {
            i = GetRandomIndex();
        }

        swipeController.previousCharacterIndex = i;

        image.sprite = characterTexture[i];
    }

    int GetRandomIndex()
    {
        return (int)Random.Range(0f, 3f);
    }
}
