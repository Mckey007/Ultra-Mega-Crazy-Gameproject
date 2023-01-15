using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollection : MonoBehaviour
{
    private GameObject player;
    private const int PLAYER_DISTANCE = 50;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        transform.position = new Vector2(player.transform.position.x - PLAYER_DISTANCE, player.transform.position.y);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        DestroyObject(collision.gameObject);
    }
}
