using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField]
    private GameObject[] spawnPoints;
    [SerializeField]
    private GameObject[] obstaclePrefabs;
    [SerializeField]
    private float delayMin = 1f, delayMax = 3f;

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
            spawnDelay = Random.Range(delayMin, delayMax);

            nextSpawnTime = Time.time + spawnDelay;
            int spIndex = Random.Range(0, spawnPoints.Length);
            if (spawnPoints.Length > 0) {
                Debug.Log(spIndex);
                Debug.Log(obstaclePrefabs[spIndex]);
                int obIndex = Random.Range(0, obstaclePrefabs.Length);
                Instantiate(obstaclePrefabs[spIndex], spawnPoints[spIndex].transform.position, Quaternion.identity);
                //if (obstaclePrefabs.Length > 0) {
                //    Debug.Log(obIndex);
                //    Debug.Log(obstaclePrefabs[obIndex]);
                //    Instantiate(obstaclePrefabs[obIndex], spawnPoints[spIndex].transform.position, Quaternion.identity);
                //}
            }
        }
    }
}
