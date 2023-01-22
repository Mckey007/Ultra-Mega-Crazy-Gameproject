using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_MINIMUM_DISTANCE = 20f;
    private const int START_INSTANCE_NUMBER = 0;
    private GameObject player;
    [SerializeField]
    private Transform levelStart;
    [SerializeField]
    private List<Transform> tileList;

    private Vector3 lastEndPosition;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        lastEndPosition = levelStart.Find("EndPoint").position;
        for(int i=0; i<START_INSTANCE_NUMBER; i++) {
            SpawnLevelPart();
        }
    }

    private void Update() {
        if(Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_MINIMUM_DISTANCE) {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart() {
        lastEndPosition = SpawnLevelPart(tileList[Random.Range(0, tileList.Count)] ,lastEndPosition).Find("EndPoint").position;
    }

    private Transform SpawnLevelPart(Transform tile, Vector3 spawnPosition) {
        Transform lastTile = Instantiate(tile, spawnPosition, tile.rotation);
        Object.Destroy(lastTile.gameObject, 10.0f);
        return lastTile;
    }
}
