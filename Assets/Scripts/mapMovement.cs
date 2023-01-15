using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private Vector3 direction = new Vector3(-1f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move() {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }
}
