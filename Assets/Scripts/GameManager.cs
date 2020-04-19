using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public GameObject gameOverUI;

    [SerializeField]
    private Text score;
    [SerializeField]
    private Spawner[] spawnPoints;

    static GameManager instance = null;
    bool gameIsOver = false;
    int curScore = 0;
    float nextAddScoreTime;
    
    void Start() {
        if (null == instance) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        gameOverUI.SetActive(false);
        gameIsOver = false;
        curScore = 0;
        nextAddScoreTime = Time.time + 1;
    }
    
    void Update() {
        if (!gameIsOver && Time.time > nextAddScoreTime) {
            nextAddScoreTime = Time.time + 1;
            curScore += 1;
            score.text = "Score: " + curScore;
        }
    }

    public static GameManager GetInstance() {
        Debug.Log(instance);
        return instance;
    }

    public void gameOver() {
        Debug.Log("Game Over");
        gameOverUI.SetActive(true);
        stopAllObstacles();
        stopPlayerInput();
        gameIsOver = true;
    }

    void stopPlayerInput() {
        if (GameObject.FindWithTag("Player") == null) {
            Debug.LogError("Player not found");
            return;
        }
        PlayerController pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (pc) {
            pc.setGameIsOver(true);
        }
    }

    void stopAllObstacles() {
        // stop spawning
        foreach (var sp in spawnPoints) {
            sp.setGameIsOver(true);
        }
        // stop obstacles' movement
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach(var ob in obstacles) {
            if (ob.GetComponent<Obstacle>()) {
                ob.GetComponent<Obstacle>().stopMovement();
            }
        }
        // stop bg movemment
        GameObject[] bgs = GameObject.FindGameObjectsWithTag("Background");
        foreach(var bg in bgs) {
            if (bg.GetComponent<Mover>()) {
                bg.GetComponent<Mover>().stop();
            }
        }
    }

    public void restart() {
        SceneManager.LoadScene("MainScene");
    }
}
