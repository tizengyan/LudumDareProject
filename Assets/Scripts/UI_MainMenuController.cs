using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject swipeMenu;
    public void LoadSwipeScene()
    {
        swipeMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
}
