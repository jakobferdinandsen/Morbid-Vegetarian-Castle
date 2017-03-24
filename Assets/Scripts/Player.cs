using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Rigidbody2D playerObject;

    // Use this for initialization
    void Start() {
        playerObject = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D coll) { }
}