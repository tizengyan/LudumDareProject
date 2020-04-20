using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject swipeMenu;
    public GameObject pauseMenu;
    public GameObject creditsPage;
    public GameObject gameOverPage;
    public GameObject gameWinPage;

    public CharacterRandomizer characterController;

    public bool isPaused;

    [SerializeField]
    private TextMeshPro finalScore;

    public void LoadSwipeScene()
    {
        swipeMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    void Start() {
        if (mainMenu.activeSelf) {
            Time.timeScale = 0;
        }
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
        creditsPage.SetActive(false);
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

    public void GameOver()
    {
        characterController.CleanGameData();

        gameOverPage.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        int score = GameManager.GetInstance().GetCurScore();
        // find score obj
        //finalScore.text = "YOUR SCORE\n" + score;
        GameObject scoreObj = gameOverPage.transform.GetChild(3).gameObject;
        TextMeshProUGUI scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
        Debug.Log("get score obj " + scoreObj + " get score text " + scoreText);
        if (scoreText) {
            Debug.Log("set score text " + scoreText);
            scoreText.text = "YOUR SCORE\n" + score;
        }
    }

    public void GameWin()
    {
        characterController.CleanGameData();

        gameWinPage.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Restart()
    {
        gameOverPage.SetActive(false);
        gameWinPage.SetActive(false);
        swipeMenu.SetActive(false);
        creditsPage.SetActive(false);
        mainMenu.SetActive(true);
        
        Time.timeScale = 1;
        isPaused = false;
    }
}
