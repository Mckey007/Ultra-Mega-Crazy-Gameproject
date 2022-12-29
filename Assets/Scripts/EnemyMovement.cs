using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float direction = -1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move() {
        if(rb.velocity.x == 0) {
            this.direction = -this.direction;
        }
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }


    private void OnCollisionEnter2D(Collision2D collision) {
         switch(collision.gameObject.tag) {
            case "Player":
                this.direction = -this.direction;
                break;
            default:
                break;
        }
    }
}
