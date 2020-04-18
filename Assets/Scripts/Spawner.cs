using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField]
    public GameObject[] obstaclePrefabs;

    float spawnDelay = 1.5f;
    float nextSpawnTime;
    bool gameIsOver = false;

    public void setGameIsOver(bool isOver) {
        gameIsOver = isOver;
    }

    void Start() {
        nextSpawnTime = Time.time + spawnDelay;
    }
    
    void Update() {
        if (!gameIsOver && Time.time > nextSpawnTime) {
            spawnDelay = Random.Range(1f, 3f);
            nextSpawnTime = Time.time + spawnDelay;
            int obIndex = Random.Range(0, obstaclePrefabs.Length - 1);
            Debug.Log(obIndex);
            Debug.Log(obstaclePrefabs[obIndex]);
            if (obstaclePrefabs[obIndex]) {
                Instantiate(obstaclePrefabs[obIndex]);
            }
        }
    }
}
