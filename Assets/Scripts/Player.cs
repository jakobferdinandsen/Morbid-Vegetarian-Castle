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

        if (Input.GetMouseButtonDown(0)) {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            GameObject bullet = (GameObject) Instantiate(Resources.Load("bullet"));
            Vector2 normalizedDirection = direction.normalized;
            bullet.transform.position = new Vector3(transform.position.x + normalizedDirection.x,
                transform.position.y + normalizedDirection.y);
            direction.Normalize();
            bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D coll) { }
}