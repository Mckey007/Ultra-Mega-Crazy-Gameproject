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

    private bool isGrounded = false;
    private bool canDoubleJump = false;
    private bool isFreezed = false;
    

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

    private BoxCollider2D boxCollider2D;
    [SerializeField] float groundCheckHeight = 0.5f;
    [SerializeField] LayerMask groundLayer;
    private float deathPosition = -50f;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        startTime = System.DateTime.Now;
        Time.timeScale = 1;
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        if(transform.position.y < deathPosition) {
            onHit();
        }

        //Movement horizontal
        move();

        //Jump
        if(Input.GetKeyDown(KeyCode.Space))
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
        CheckGrounded();

    }

    private void CheckGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, groundCheckHeight, groundLayer);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            isGrounded = true;
            canDoubleJump = true;
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
            isGrounded = false;
        }
        //Debug.DrawR(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + groundCheckHeight), rayColor);
        
    }

    void move() {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void jump() {
        if (!isGrounded && !canDoubleJump) return;
        if (!isGrounded && canDoubleJump) canDoubleJump = false;
        PlayJumpSound();
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
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
            case "Enemy":
                onHit();
                break;
            default:
                break;
        }
    }

    public void UpdateDeathPosition(float y)
    {
        deathPosition = y - 50f;
    }
}
