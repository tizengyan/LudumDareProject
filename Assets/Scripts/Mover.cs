using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    [SerializeField]
    private float leftPosX = -15f, rightPosX = 75;
    [SerializeField]
    private float speed = -1f;
    [SerializeField]
    private bool isSpeedSyncWithGM = false;
    [SerializeField]
    private bool isCycle = true;
    [SerializeField]
    private bool isNeedDelay = true;

    float startDelay = -1f;
    bool gameIsOver = false;

    void Start() {
        startDelay = GameManager.GetInstance().GameStartDelay();
        if (isSpeedSyncWithGM) {
            float speedRatio = GameManager.GetInstance().GetSpeedRatio();
            float speedBase = GameManager.GetInstance().GetSpeedBase();
            speedBase = Mathf.Clamp(speedBase, 0.1f, 10f);
            speed *= speedBase + speedRatio;
            Debug.Log(gameObject.name + "'s speed ratio is " + speedRatio);
        }
    }
    
    void Update() {
        if (!gameIsOver) {
            if (!isNeedDelay || startDelay < 0) {
                if (transform.position.x > leftPosX) {
                    transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                }
                else if (isCycle) {
                    transform.position = new Vector2(rightPosX, transform.position.y);
                }
            }
            else {
                startDelay -= Time.deltaTime;
            }
        }
    }

    public void Stop() {
        gameIsOver = true;
    }
}
