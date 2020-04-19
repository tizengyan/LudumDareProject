using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanColour : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] trashcanTexture;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        int i = (int)Random.Range(0f, 3f);

        spriteRenderer.sprite = trashcanTexture[i];
    }


}
