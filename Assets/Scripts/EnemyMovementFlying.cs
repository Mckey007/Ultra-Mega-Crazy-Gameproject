using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementFlying : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float detectionRange;
    private bool playerDetected;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(playerDetected == false && Vector2.Distance(player.transform.position, transform.position) <= detectionRange)
        {
            playerDetected = true;
        }

        if(player != null && playerDetected == true)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
