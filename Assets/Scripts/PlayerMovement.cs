using System;
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

    private DateTime startTime;
    private DateTime endTime;
    [SerializeField] TMP_Text timer;
    private bool gameOver;


    void Start()
    {
        startTime = System.DateTime.Now;
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
        if(Input.GetKey(KeyCode.Space) && jumpsLeft > 0)
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
        // float horizontal = 1;
        // rb.velocity = new Vector2(speed * boost, rb.velocity.y);
        transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
    }

    void jump() {
        PlayJumpSound();
        //rb.velocity= new Vector2(rb.velocity.x, jumpHeight);
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

        if(gameOver == false)
        {
            endTime = System.DateTime.Now;
            TimeSpan playTime = endTime - startTime;
            timer.text = String.Format("{0}m:{1:D2}s", playTime.Minutes, playTime.Seconds);
        }
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        this.isFreezed = true;
        gameOver = true;
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
                //boost = 1f;
                break;
            case "Enemy":
                onHit();
                break;
            default:
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        switch(other.gameObject.tag) {
            case "Ground":
                //isGrounded = false;
                //boost = 0.7f;
                break;
            default:
                break;
        }
    }
}
