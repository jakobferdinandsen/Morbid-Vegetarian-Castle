using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour{
    public float speed;

    private Transform target;

    private bool cooldown;

    private int bulletSpeed;
    private Boolean waiting;
    private float timestamp;

    // Use this for initialization
    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        cooldown = false;
        bulletSpeed = 20;
    }

    void Update() {
        if (Time.time - timestamp > 1) {
            waiting = false;
        }

        if (!waiting) {
            Vector2 direction = target.position - transform.position;
            GameObject bullet = (GameObject) Instantiate(Resources.Load("EnemyBullet"));
            Vector2 normalizedDirection = direction.normalized;
            bullet.transform.position = new Vector3(transform.position.x + normalizedDirection.x,
                transform.position.y + normalizedDirection.y);
            direction.Normalize();
            gameObject.GetComponent<SpriteRenderer>().flipX = direction.x < 0;
            float angle = (float) Math.Atan(direction.x / direction.y);
            if (direction.y > 0 || direction.x > 0) {
                bullet.transform.eulerAngles = new Vector3(0, 0, angle * 57.2958f * -1);
            }
            else {
                bullet.transform.eulerAngles = new Vector3(0, 0, angle * 57.2958f * -1 -180);
            }
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            timestamp = Time.time;
            waiting = true;
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("bullet")) {
            timestamp = Time.time;
            waiting = true;
        }
    }
}