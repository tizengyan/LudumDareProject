using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPImageReplace : MonoBehaviour
{
    public Image image;
    public Sprite[] hpImage;
    public int currentHealth;

    void Start() {
        StartCoroutine(RefreshImg());
    }

    IEnumerator RefreshImg() {
        while (true) {
            currentHealth = GameManager.GetInstance().GetHP() + 1;
            if (currentHealth < hpImage.Length) {
                image.sprite = hpImage[currentHealth];
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
