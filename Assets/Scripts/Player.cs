using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    public float speed;

    private Rigidbody2D playerObject;
    private GameObject sword;
    private Boolean swinging;

    // Use this for initialization
    void Start() {
        playerObject = GetComponent<Rigidbody2D>();
        sword = GameObject.FindWithTag("playerSword");
        sword.GetComponent<BoxCollider2D>().enabled = false;
        sword.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update() {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;

        if (Input.GetMouseButtonDown(1)) {
            swinging = true;
        }

        if (Input.GetMouseButtonDown(0)) {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            GameObject bullet = (GameObject) Instantiate(Resources.Load("bullet"));
            Vector2 normalizedDirection = direction.normalized;
            bullet.transform.position = new Vector3(transform.position.x + normalizedDirection.x,
                transform.position.y + normalizedDirection.y);
            direction.Normalize();
            bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }

        if (swinging) {
            if (sword.GetComponent<BoxCollider2D>().enabled == false) {
                sword.transform.localEulerAngles = new Vector3(0, 0, 330);
                sword.transform.localPosition = new Vector3(0.847f, -0.046f, 0);
            }
            sword.GetComponent<BoxCollider2D>().enabled = true;
            sword.GetComponent<SpriteRenderer>().enabled = true;

            sword.transform.localEulerAngles = new Vector3(0, 0,
                sword.transform.localEulerAngles.z - 400 * Time.deltaTime);

            if (sword.transform.localEulerAngles.z < 210) {
                swinging = false;
                sword.GetComponent<BoxCollider2D>().enabled = false;
                sword.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
    }
}