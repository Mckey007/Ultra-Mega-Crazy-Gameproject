using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpHeight = 5f;
    [SerializeField]
    private int maxJumps = 2;

    private bool isGrounded = false;
    private bool isFreezed = false;
    private int jumpsLeft;
    public float timescale;
    

    // animation & sound
    Animator animator;
    private string currentState;

    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_WALK = "Player_Walk";
    const string PLAYER_JUMP = "Player_Jump";

    [SerializeField] private AudioClip jumpSFX;
    private float jumpSxfTimeLastPlayed = 0f;

    [SerializeField] GameObject gameOverScreen;


    void Start()
    {
        Time.timeScale = 1;
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpsLeft = maxJumps;
    }

    void Update()
    {
        //Freeze handle
        if(this.isFreezed == true) {
            if(Input.GetKey(KeyCode.Space)) {
                reset();
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                loadMainMenu();
            }
        }

        //Reset handle
        if(transform.position.y < -50f) {
            onHit();
        }

        //Movement horizontal
        move();

        //Jump
        if(Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            jump();
        }

        if(isGrounded)
        {
            ChangeAnimationState(PLAYER_WALK);
        }
        else
        {
            ChangeAnimationState(PLAYER_JUMP);
        }

    }

    void move() {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void jump() {
        PlayJumpSound();
        if(jumpsLeft < maxJumps) {
            rb.velocity = Vector2.zero;
        }
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        jumpsLeft--;
        isGrounded = false;
    }

    void PlayJumpSound()
    {
        // prevents the jump sound from playing too often and interrupting itself
        if (jumpSxfTimeLastPlayed + jumpSFX.length >= Time.time) return;
        jumpSxfTimeLastPlayed = Time.time;
        SoundManager.PlaySoundOnce(jumpSFX);
    }

    public void onHit() {
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        this.isFreezed = true;
    }

    private void loadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void reset() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ChangeAnimationState(string newState)
    {
        // dont allow the same animation to interrupt itself
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }

    private void OnCollisionEnter2D(Collision2D collision) {   
        switch(collision.gameObject.tag) {
            case "Ground":
                isGrounded = true;
                jumpsLeft = maxJumps;
                break;
            case "Enemy":
                onHit();
                break;
            default:
                break;
        }
    }
}
