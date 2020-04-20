using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    [SerializeField]
    private float speed = -5f;
    [SerializeField]
    private float endPointX = -10f;

    bool isStoped = false;

    void Start() {

    }
    
    void Update() {
        if (!isStoped) {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        //Debug.Log(transform.position);
        //Debug.Log(speed * Time.deltaTime);
        if (transform.position.x < endPointX) {
            Destroy(gameObject);
        }
    }

    public void StopMovement() {
        isStoped = true;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        
    }
}
