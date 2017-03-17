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

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            
        }
        if (Input.GetKey(KeyCode.RightArrow)) {

        }
        if (Input.GetKey(KeyCode.UpArrow)) {

        }
        if (Input.GetKey(KeyCode.DownArrow)) {

        }
    }

    void OnCollisionEnter2D(Collision2D coll) { }
}