using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float jumpStrength = 1f;
    [SerializeField]
    private int hitPoint = 3;

    float startDelay = 2f;
    bool gameIsOver = false;
    bool isGrounded = false;
    Rigidbody2D rb2d;
    Animator animator;

    public void SetGameIsOver(bool isOver) {
        gameIsOver = isOver;
    }

    void Start() {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        gameIsOver = false;
        startDelay = GameManager.GetInstance().GameStartDelay();
        Invoke("Run", startDelay);
    }
    
    void Update() {
        if (!gameIsOver) {
            if (startDelay < 0) {
                if (rb2d && Input.GetKeyDown("space")) {
                    rb2d.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
                }
            }
            else {
                startDelay -= Time.deltaTime;
            }
        }
    }

    void Run() {
        animator.SetBool("isRunning", true);
        animator.SetBool("isJumping", false);
    }

    void Die() {
        // animator.SetBool("isDead", true);
        GameManager.GetInstance().GameOver();
        animator.SetBool("gameIsOver", true);
    }

    void Damage() {
        hitPoint -= 1;
        if (hitPoint <= 0) {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Obstacle") {
            GameManager.GetInstance().GameOver();
            animator.SetBool("gameIsOver", true);
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
