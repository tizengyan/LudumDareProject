using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    [SerializeField]
    private float leftPosX = -15f, rightPosX = 75;
    [SerializeField]
    private float speed = -1f;
    [SerializeField]
    private bool isCycle = true;

    float startDelay = -1f;
    bool gameIsOver = false;

    void Start() {
        startDelay = GameManager.GetInstance().GameStartDelay();
        //Debug.Log("Mover start delay = " + startDelay);
    }
    
    void Update() {
        if (!gameIsOver) {
            if (startDelay < 0) {
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
