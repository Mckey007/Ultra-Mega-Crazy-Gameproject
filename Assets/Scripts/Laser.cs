using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private int detectionRange;
    public LineRenderer lineRenderer;
    private GameObject player;
    private Vector2 lookDirection;
    private float lookAngle;
    private bool playerDetected;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerDetected = false;
    }
    // Update is called once per frame
    void Update()
    {
        lookDirection = player.transform.position - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, lookAngle);
        if (Vector2.Distance(player.transform.position, transform.position) <= detectionRange)
        {
            playerDetected = true;
            EnableLaser();
            StartCoroutine(Shoot());
        }
        else
        {
            playerDetected = false;
            DisableLaser();
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(2);
        if (playerDetected)
        {
            lineRenderer.startWidth = 0.5f;
            lineRenderer.endWidth = 0.5f;
            yield return new WaitForSeconds(0.5f);
            player.GetComponent<PlayerMovement>().onHit();
        }
    }

    void EnableLaser()
    {
        lineRenderer.enabled = true;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.SetPosition(0, firePoint.transform.position);
        lineRenderer.SetPosition(1, player.transform.position);
    }

    void DisableLaser()
    {
        lineRenderer.enabled = false;
    }

}
