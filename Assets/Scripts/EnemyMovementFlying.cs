using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

        // math magic to make the enemy look toward the player
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
