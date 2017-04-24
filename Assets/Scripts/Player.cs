using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    public float speed;

    private Rigidbody2D playerObject;
    private GameObject sword;
    private Boolean swinging;

    //Collections
    private int BabySalatsCollected = 0;
    private int yellowKeyCollected = 0;
    private int greenKeyCollected = 0;
    private int blueKeyCollected = 0;
    private int redKeyCollected = 0;


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
            bullet.GetComponent<Rigidbody2D>().velocity = direction * speed*2;
        }

        if (swinging) {
            if (sword.GetComponent<BoxCollider2D>().enabled == false) {
                sword.transform.localEulerAngles = new Vector3(0, 0, 330);
                sword.transform.localPosition = new Vector3(1.61f, -0.046f, 0);
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

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Back_to_level_1")
        {
            Application.LoadLevel(0);
            Debug.Log("Entered door to Level_1");
            Debug.Log(coll.transform.position);
        }
        else if (coll.gameObject.tag == "Door_1")
        {
            Application.LoadLevel(1);
            Debug.Log("Entered door to Level_1_Part2");
        }
        else if (coll.gameObject.tag == "Back_to_level_2")
        {
            Application.LoadLevel(2);
            Debug.Log("Entered door to Level_2");
        }
        else if (coll.gameObject.tag == "Door_2")
        {
            Application.LoadLevel(3);
            Debug.Log("Entered door to Level_2_Part2");
        }

        //Baby Salats
        if (coll.gameObject.tag == "Baby_salat")
        {
            Destroy(coll.gameObject);
            BabySalatsCollected++;
            GameObject.FindGameObjectWithTag("Baby_salat_text").GetComponent<Text>().text = BabySalatsCollected + "/3";
            Debug.Log("Baby_Salat Collected " + BabySalatsCollected + "/3");
        }

        //Yellow Key
        if (coll.gameObject.tag == "yellowKey")
        {
            Destroy(coll.gameObject);
            yellowKeyCollected++;
            GameObject.FindGameObjectWithTag("Yellow_Key_Text").GetComponent<Text>().text = yellowKeyCollected + "/1";
            Debug.Log("Yellow_Key Collected " + yellowKeyCollected + "/1");
        }

        //Green Key
        if (coll.gameObject.tag == "greenKey")
        {
            Destroy(coll.gameObject);
            greenKeyCollected++;
            GameObject.FindGameObjectWithTag("Green_Key_Text").GetComponent<Text>().text = greenKeyCollected + "/1";
            Debug.Log("Green_Key Collected " + greenKeyCollected + "/1");
        }

        if (coll.gameObject.tag == "Enemy") {
            Destroy(gameObject);
        }
    }
}