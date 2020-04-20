using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPImageReplace : MonoBehaviour
{
    public Image image;
    public Sprite[] hpImage;
    public int currentHealth;

    // Update is called once per frame
    void Update()
    {
        image.sprite = hpImage[currentHealth];
    }
}
