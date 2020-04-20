using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SwipeController : MonoBehaviour
{
    public Animator characterAnimator;
    public GameObject tutorialMenu;

    private void Awake() {
        characterAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void swipeNo ()
    {
        characterAnimator.SetTrigger("TriggerNo");
    }

    public void swipeYes()
    {
        characterAnimator.SetTrigger("TriggerYes");
        //StartCoroutine("StartTutorial");
    }

    public void ShowTutorial() {
        tutorialMenu.SetActive(true);
    }

    IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(1f);
        tutorialMenu.SetActive(true);
    }

    private void Update()
    {
        StartGame();
    }

    public void StartGame()
    {
        if (tutorialMenu.activeSelf && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Start game!");
            Time.timeScale = 1;
            //gameObject.SetActive(false);
            DataManager.IsGameStart = true;
            GameManager.GetInstance().Restart();
        }
    }
}
