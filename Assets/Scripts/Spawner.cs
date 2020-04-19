using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField]
    private GameObject[] spawnPoints;
    [SerializeField]
    private GameObject[] obstaclePrefabs;
    [SerializeField]
    private float intervalMin = 1f, intervalMax = 3f;

    float startDelay = 2f;
    float spawnInterval = 2.5f;
    bool gameIsOver = false;

    public void setGameIsOver(bool isOver) {
        gameIsOver = isOver;
    }

    void Start() {
        startDelay = GameManager.GetInstance().GameStartDelay();
        Invoke("BeginSpawn", startDelay);
    }

    void BeginSpawn() {
        StartCoroutine("Spawning");
    }

    IEnumerator Spawning() {
        while (!gameIsOver) {
            spawnInterval = Random.Range(intervalMin, intervalMax);
            int spIndex = Random.Range(0, spawnPoints.Length);
            if (spawnPoints.Length > 0) {
                //Debug.Log("spIndex = " + spIndex);
                //Debug.Log(obstaclePrefabs[spIndex]);
                Instantiate(obstaclePrefabs[spIndex], spawnPoints[spIndex].transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    void Update() {

    }
}
