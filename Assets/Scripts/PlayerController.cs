using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float jumpStrength = 1f;
    [SerializeField]
    private int hitPoint = 3;
    [SerializeField]
    private float footStepFrequency = 0.2f;
    [SerializeField]
    private AudioClip jumpSound, landSound, hitSound, dieSound;

    float startDelay = 2f;
    bool gameIsOver = false;
    bool isGrounded = false;
    Coroutine coroutineStepSound = null;
    Rigidbody2D rb2d;
    Animator animator;
    AudioSource audioSource;

    [SerializeField]
    float invincibleAfterHurtTime = 0.5f;
    float invincibleTimer;
    bool isInvincible;

    public void SetGameIsOver(bool isOver) {
        gameIsOver = isOver;
    }

    public int GetHP() {
        return hitPoint;
    }

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        startDelay = GameManager.GetInstance().GameStartDelay();
        //Debug.Log("start delay = " + startDelay);
        //Invoke("Run", startDelay);
        StartCoroutine(Run(startDelay));
    }
    
    void Update() {
        if (!gameIsOver) {
            if (startDelay < 0) {
                if (rb2d && isGrounded && Input.GetKeyDown("space")) {
                    Jump();
                }
            }
            else {
                startDelay -= Time.deltaTime;
            }
        }

        if (Time.time >= invincibleTimer + invincibleAfterHurtTime)
        {
            isInvincible = false;
        }

    }

    public void Triumph() {
        BeInvincible();
        StartCoroutine(MoveToTheDoor());
    }

    IEnumerator MoveToTheDoor() {
        float speed = 10f;
        float speedBase = GameManager.GetInstance().GetSpeedBase();
        float speedRiseRatio = GameManager.GetInstance().GetSpeedRatio();
        speed *= speedBase + speedRiseRatio;
        while (true) {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            // enter the door
            if (transform.position.x > 20 + 1) {
                //callback
                break;
            }
            yield return null;
        }
    }

    void BeInvincible() {
        isInvincible = true;
        invincibleAfterHurtTime = 100;
    }

    IEnumerator Run(float delay) {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isRunning", true);
        animator.SetBool("isJumping", false);
        if (coroutineStepSound == null) {
           coroutineStepSound = StartCoroutine("PlayFootStepSound");
        }
    }

    void Jump() {
        Debug.Log("Jumping");
        audioSource.PlayOneShot(jumpSound);
        rb2d.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        if (coroutineStepSound != null) {
            StopCoroutine("PlayFootStepSound");
            coroutineStepSound = null;
        }
        animator.SetBool("isJumping", true);
    }

    public void Stop() {
        gameIsOver = true;
        if (coroutineStepSound != null) {
            StopCoroutine("PlayFootStepSound");
            coroutineStepSound = null;
        }
        //rb2d.isKinematic = true;
    }

    IEnumerator PlayFootStepSound() {
        while (!gameIsOver) {
            audioSource.Play();
            yield return new WaitForSeconds(footStepFrequency);
        }
    }

    void Die() {
        animator.SetBool("gameIsOver", true);
        animator.SetTrigger("Dead");
        Debug.Log("Dead");
        audioSource.PlayOneShot(dieSound, 1f);
        GameManager.GetInstance().StartCoroutine("GameOver");
    }

    void Damage() {

        Debug.Log("Damaged");

        isInvincible = true;
        invincibleTimer = Time.time;


        if (hitPoint > 0) {
            audioSource.PlayOneShot(hitSound);
            hitPoint -= 1;

            if (isGrounded)
            {
                animator.SetTrigger("hit");
            }
            else
            {
                animator.SetTrigger("hitAir");
            }

        }
        else {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Obstacle" && !isInvincible) {
            Damage();
        }
        else if (collision.gameObject.tag == "Ground") {
            Debug.Log("Hit ground");
            audioSource.PlayOneShot(landSound);
            isGrounded = true;
            //animator.SetBool("isJumping", false);
            if (startDelay < 0) {
                StartCoroutine(Run(0));
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }
}
