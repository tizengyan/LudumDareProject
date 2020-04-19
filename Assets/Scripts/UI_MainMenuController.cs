using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject swipeMenu;
    public GameObject pauseMenu;
    public GameObject creditsPage;

    public bool isPaused;

    public void LoadSwipeScene()
    {
        swipeMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();                   
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        pauseMenu.SetActive(false) ;
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    public void CreditsOpen()
    {
        creditsPage.SetActive(true);
    }

    public void CreditsClose()
    {
        creditsPage.SetActive(false);
    }
}
