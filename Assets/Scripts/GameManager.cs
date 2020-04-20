using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private Text score;
    [SerializeField]
    private Spawner[] spawnPoints;
    [SerializeField]
    private float gameStartDelay = 2f;
    [SerializeField]
    private int scoreDemand = 10, levelLimit = 10;

    static GameManager instance = null;
    bool gameIsOver = false;
    int curScore = 0;
    static int curLevel = 1;
    float nextAddScoreTime;

    public float GameStartDelay() => gameStartDelay;
    public int GetCurScore() => curScore;

    public int GetHP() {
        GameObject player = GameObject.FindWithTag("Player");
        if (player) {
            return player.GetComponent<PlayerController>().GetHP();
        }
        return 0;
    }

    void Awake() {
        Debug.Log("Awake");
        if (null == instance) {
            instance = this;
        }
        else if (instance != this) {
            Debug.LogWarning("Destroy duplicate gm");
            Destroy(gameObject);
        }
    }

    void Start() {
        Debug.Log("Start");
        //gameIsOver = false;
        curScore = 0;
        nextAddScoreTime = Time.time + 1;
        GameStart();
    }
    
    void Update() {
        
    }

    void GameStart() {
        StartCoroutine("AddScore");
    }

    IEnumerator AddScore() {
        yield return new WaitForSeconds(gameStartDelay);
        while (!gameIsOver) {
            Debug.Log("gameisOver " + gameIsOver);
            curScore += 1;
            score.text = "Score: " + curScore;
            Debug.Log("Setting score to " + curScore);
            if (curScore >= scoreDemand) {
                levelClear();
            }
            yield return new WaitForSeconds(1);
        }
    }

    public static GameManager GetInstance() {
        return instance;
    }

    public void RefreshHitPoint() {

    }

    public void GameOver() {
        Debug.Log("Game Over");
        DataManager.TotalScore += curScore;
        mainMenu.GetComponent<UI_MainMenuController>().GameOver();
        StopCoroutine("AddScore");
        StopAllObstacles();
        StopPlayer();
        gameIsOver = true;
    }

    public void StopGame() {
        Debug.Log("Stop game");
        StopCoroutine("AddScore");
        StopAllObstacles();
        StopPlayer();
        gameIsOver = true;
    }

    void StopPlayer() {
        if (GameObject.FindWithTag("Player") == null) {
            Debug.LogError("Player not found");
            return;
        }
        PlayerController pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (pc) {
            pc.Stop();
        }
    }

    void StopAllObstacles() {
        // stop spawning
        foreach (var sp in spawnPoints) {
            sp.setGameIsOver(true);
        }
        // stop obstacles' movement
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach(var ob in obstacles) {
            if (ob.GetComponent<Obstacle>()) {
                ob.GetComponent<Obstacle>().StopMovement();
            }
        }
        // stop bg movemment
        GameObject[] bgs = GameObject.FindGameObjectsWithTag("Background");
        foreach(var bg in bgs) {
            if (bg.GetComponent<Mover>()) {
                bg.GetComponent<Mover>().Stop();
            }
        }
    }

    public void Restart() => SceneManager.LoadScene("MainScene");

    void levelClear() {
        Debug.Log("level clear " + curLevel);
        curLevel += 1;
        DataManager.CurLevel = curLevel;
        DataManager.TotalScore += curScore;
        if (curLevel >= levelLimit) {
            mainMenu.GetComponent<UI_MainMenuController>().GameWin();
        }
        // goto next level
        mainMenu.GetComponent<UI_MainMenuController>().LoadSwipeScene();
        StopCoroutine("AddScore");
        StopAllObstacles();
        StopPlayer();
    }
}
