using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Rigidbody playerObject;

    // Use this for initialization
    void Start() {
        playerObject = GetComponent<Rigidbody>();
    }

    void Update() {
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += move * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D coll) { }
}