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
    [SerializeField]
    private float offsetY = 3f;
    [SerializeField]
    private bool[] isNeedOffset;

    float startDelay = 2f;
    public float spawnInterval = 1.8f;
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
            float roll = Random.Range(0f, 1f);
            int spIndex = 0;
            if (roll > 0.5) {
                roll = Random.Range(0, 1f);
                if (roll > 0.5) {
                    spIndex = 1;
                }
                else {
                    spIndex = 2;
                }
            }
            //int spIndex = Random.Range(0, spawnPoints.Length);
            if (spawnPoints.Length > 0) {
                //Debug.Log("spIndex = " + spIndex);
                //Debug.Log(obstaclePrefabs[spIndex]);
                Vector3 spawnPos = spawnPoints[spIndex].transform.position;
                if (spIndex < isNeedOffset.Length && isNeedOffset[spIndex]) {
                    spawnPos = new Vector3(spawnPos.x, spawnPos.y + Random.Range(-offsetY, offsetY), spawnPos.z);
                }
                Instantiate(obstaclePrefabs[spIndex], spawnPos, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    void Update() {

    }
}
