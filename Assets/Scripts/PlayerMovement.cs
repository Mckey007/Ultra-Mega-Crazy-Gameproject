using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float gravity = -9.8f;
    [SerializeField]
    private float jumpHeight = 10f;

    private bool isGrounded = false;
    private bool isFreezed = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Freeze handle
        if(this.isFreezed == true) {
            if(Input.GetKey(KeyCode.Space)) {
                reset();
            }
        }

        //Reset handle
        if(transform.position.y < -10f) {
            reset();
        }

        //Movement horizontal
        move();

        //Jump
        if(Input.GetKey(KeyCode.Space) && isGrounded)
        {
            jump();
        }

    }

    void move() {
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void jump() {
        rb.velocity= new Vector2(rb.velocity.x, jumpHeight);
    }

    void onHit() {
        Time.timeScale = 0;
        this.isFreezed = true;
    }

    void reset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision) {   
        switch(collision.gameObject.tag) {
            case "Ground":
                isGrounded = true;
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
                isGrounded = false;
                break;
            default:
                break;
        }
    }
}

        ////DEPRECADED
        //float vertical = Input.GetAxis("Vertical"); // W = 1, S = -1
        //float horizontal = Input.GetAxis("Horizontal"); // A = -1 , D = 1
        //Vector3 moveDirection = new(horizontal, 0, 0);
        //// groundedCheck

        //if(true) { // grounded?
        //    if(vertical > 0) // jump
        //    {
        //        //moveDirection.y = jumpHeight;
        //        // grounded = false;
        //    }
        //}
        //else
        //{
        //    moveDirection.y = gravity;
        //}
        

        //// Todo FixedUpdate?
        //rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);