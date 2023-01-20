using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate = 1f;
    private bool canFire = true;
    private Vector2 lookDirection;
    private float lookAngle;

    public float LookAngle { get => lookAngle; }
    public Transform FirePoint { get => firePoint; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, lookAngle);

        if (Input.GetButtonDown("Fire1") && canFire)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            canFire = false;
            StartCoroutine(ShootDelay());
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}
