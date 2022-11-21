using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float gravity = -9.8f;
    [SerializeField]
    private float jumpHeight = 20f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical"); // W = 1, S = -1
        float horizontal = Input.GetAxis("Horizontal"); // A = -1 , D = 1
        Vector3 moveDirection = new(horizontal, 0, 0);

        // groundedCheck

        if(true) { // grounded?
            if(vertical > 0) // jump
            {
                moveDirection.y = jumpHeight;
                // grounded = false;
            }
        }
        else
        {
            moveDirection.y = gravity;
        }
        

        // Todo FixedUpdate?
        rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);
    }
}
