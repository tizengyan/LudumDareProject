using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float jumpStrength = 1f;

    bool gameIsOver = false;
    bool isGrounded = false;
    Rigidbody2D rb2d;
    Animator animator;

    public void setGameIsOver(bool isOver) {
        gameIsOver = isOver;
    }

    void Start() {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        gameIsOver = false;
        Invoke("run", 2);
    }

    // Update is called once per frame
    void Update() {
        if (!gameIsOver && Input.GetKeyDown("space")) {
            if (rb2d) {
                rb2d.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            }
        }
    }

    void run() {
        animator.SetBool("isRunning", true);
        animator.SetBool("isJumping", false);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Obstacle") {
            GameManager.GetInstance().gameOver();
            // stop obstacle and self
            collision.gameObject.GetComponent<Obstacle>().stopMovement();
        }
        else if (collision.gameObject.tag == "Ground") {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }
}
