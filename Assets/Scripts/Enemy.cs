using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    public float speed;
    private Transform target;

    private Boolean waiting;
    private float timestamp;

    // Use this for initialization
    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        if (Time.time - timestamp > 2) {
            waiting = false;
        }

        if (!waiting) {
            float step = speed * Time.deltaTime;
            float lastX = transform.position.x;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            gameObject.GetComponent<SpriteRenderer>().flipX = transform.position.x - lastX < 0;
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("bullet")) {
            timestamp = Time.time;
            waiting = true;
        }
    }
}