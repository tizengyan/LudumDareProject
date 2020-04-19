using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float jumpStrength = 1f;
    [SerializeField]
    private int hitPoint = 3;
    [SerializeField]
    private AudioClip jumpSound, landSound, hitSound;

    float startDelay = 2f;
    bool gameIsOver = false;
    bool isGrounded = false;
    Rigidbody2D rb2d;
    Animator animator;
    AudioSource audioSource;

    public void SetGameIsOver(bool isOver) {
        gameIsOver = isOver;
    }

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameIsOver = false;
        startDelay = GameManager.GetInstance().GameStartDelay();
        Invoke("Run", startDelay);
    }
    
    void Update() {
        if (!gameIsOver) {
            if (startDelay < 0) {
                if (rb2d && isGrounded && Input.GetKeyDown("space")) {
                    audioSource.PlayOneShot(jumpSound);
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
        //audioSource.Play();
    }

    void Die() {
        Debug.Log("Dead");
        GameManager.GetInstance().GameOver();
        animator.SetBool("gameIsOver", true);
    }

    void Damage() {
        Debug.Log("Damaged");
        audioSource.PlayOneShot(hitSound);
        hitPoint -= 1;
        if (hitPoint <= 0) {
            Die();
        }
        animator.SetTrigger("hit");
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Obstacle") {
            Damage();
        }
        else if (collision.gameObject.tag == "Ground") {
            audioSource.PlayOneShot(landSound);
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
