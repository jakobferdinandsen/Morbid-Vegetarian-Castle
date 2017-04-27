using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    public float speed;

    private Rigidbody2D playerObject;

    private bool goingLeft = false;

    //Attack
    private GameObject sword;

    private Boolean swinging;
    public float attackRadius;
    private float attackRad;

    public PlayerHealth healthScript;

    //Collections
    private int babySalatsCollected = 0;

    private int yellowKeyCollected = 0;
    private int greenKeyCollected = 0;
    private int blueKeyCollected = 0;
    private int redKeyCollected = 0;


    // Use this for initialization
    void Start() {
        LoadState();
        UpdateHUD();
        playerObject = GetComponent<Rigidbody2D>();
        sword = GameObject.FindWithTag("playerSword");
        sword.GetComponent<BoxCollider2D>().enabled = false;
        sword.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update() {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if (move.x != 0) {
            goingLeft = move.x < 0;
        }
        gameObject.GetComponent<SpriteRenderer>().flipX = goingLeft;

        transform.position += move * speed * Time.deltaTime;

        //Swing
        if (Input.GetMouseButtonDown(1)) {
            swinging = true;
        }

        //Shoot
        if (Input.GetMouseButtonDown(0)) {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            GameObject bullet = (GameObject) Instantiate(Resources.Load("bullet"));
            Vector2 normalizedDirection = direction.normalized;
            bullet.transform.position = new Vector3(transform.position.x + normalizedDirection.x,
                transform.position.y + normalizedDirection.y);
            direction.Normalize();
            bullet.GetComponent<Rigidbody2D>().velocity = direction * speed * 2;
        }

        if (swinging) {
            sword.GetComponent<SpriteRenderer>().flipX = goingLeft;
            if (sword.GetComponent<BoxCollider2D>().enabled == false) {
                attackRad = (float) (0.5 * Math.PI);
            }
            sword.GetComponent<BoxCollider2D>().enabled = true;
            sword.GetComponent<SpriteRenderer>().enabled = true;
            float x = (float) (attackRadius * Math.Cos(attackRad));
            if (goingLeft) {
                x = x * -1;
                sword.transform.localEulerAngles = new Vector3(0, 0, (attackRad * 57.2958f) + 210);
            }
            else {
                sword.transform.localEulerAngles = new Vector3(0, 0, (-attackRad * 57.2958f) + 150);
            }
            float y = (float) (attackRadius * Math.Sin(attackRad)) + 0.2f;
            sword.transform.localPosition = new Vector3(-x, y, 0);

            attackRad = attackRad + 10 * Time.deltaTime;

            if (attackRad >= 1.5 * Math.PI) {
                swinging = false;
                sword.GetComponent<BoxCollider2D>().enabled = false;
                sword.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        //Baby Salads
        if (coll.gameObject.tag == "Baby_salat") {
            Destroy(coll.gameObject);
            babySalatsCollected++;
            GameObject.FindGameObjectWithTag("Baby_salat_text").GetComponent<Text>().text = babySalatsCollected + "/6";
            Debug.Log("Baby_Salat Collected " + babySalatsCollected + "/6");
        }

        //Yellow Key
        if (coll.gameObject.tag == "yellowKey") {
            Destroy(coll.gameObject);
            yellowKeyCollected++;
            GameObject.FindGameObjectWithTag("Yellow_Key_Text").GetComponent<Text>().text = yellowKeyCollected + "/1";
            Debug.Log("Yellow_Key Collected " + yellowKeyCollected + "/1");
        }

        //Green Key
        if (coll.gameObject.tag == "greenKey") {
            Destroy(coll.gameObject);
            greenKeyCollected++;
            GameObject.FindGameObjectWithTag("Green_Key_Text").GetComponent<Text>().text = greenKeyCollected + "/1";
            Debug.Log("Green_Key Collected " + greenKeyCollected + "/1");
        }

        //Green Door
        if (coll.gameObject.tag == "Green_Door" && greenKeyCollected >= 1) {
            Destroy(coll.gameObject);
        }

        //Yellow Door
        if (coll.gameObject.tag == "Yellow_Door" && yellowKeyCollected >= 1) {
            Destroy(coll.gameObject);
        }

//        if (coll.gameObject.tag == "Enemy") {
//            Destroy(gameObject);
//        }
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Back_to_level_1")) {
            SaveState();
            SceneManager.LoadScene("Level_1");
            Debug.Log("Entered door to Level_1");
        }
        else if (coll.CompareTag("Door_1")) {
            SaveState();
            SceneManager.LoadScene("Level_1_Part2");
            Debug.Log("Entered door to Level_1_Part2");
        }
        else if (coll.CompareTag("Back_to_level_2")) {
            SaveState();
            SceneManager.LoadScene("Level_2");
            Debug.Log("Entered door to Level_2");
        }
        else if (coll.CompareTag("Door_2")) {
            SaveState();
            SceneManager.LoadScene("Level_2_Part2");
            Debug.Log("Entered door to Level_2_Part2");
        }
        else if (coll.CompareTag("CompleteLevel1")) {
            if (yellowKeyCollected > 0) {
                LevelManager.firstLaunch = true;
                Destroy(GameObject.FindWithTag("LevelManager"));
                SceneManager.LoadScene("Level_2");
            }
        }else if (coll.CompareTag("CompleteLevel2")) {
            if (greenKeyCollected > 0) {
                LevelManager.firstLaunch = true;
                Destroy(GameObject.FindWithTag("LevelManager"));
                //TODO Load main menu
                SceneManager.LoadScene("Level_1");
            }
        }
    }

    private void SaveState() {
        PlayerPrefs.SetString("lastLevel", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("babySalatsCollected", babySalatsCollected);
        PlayerPrefs.SetInt("greenKeyCollected", greenKeyCollected);
        PlayerPrefs.SetInt("yellowKeyCollected", yellowKeyCollected);
        PlayerPrefs.SetInt("blueKeyCollected", blueKeyCollected);
        PlayerPrefs.SetInt("redKeyCollected", redKeyCollected);
        healthScript.SaveState();

        String savedJson = "{\"positions\":[";
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            savedJson += getPositionJson("\"enemy\"", enemy);
        }
        GameObject[] salads = GameObject.FindGameObjectsWithTag("Baby_salat");
        foreach (GameObject salad in salads) {
            savedJson += getPositionJson("\"salad\"", salad);
        }
        savedJson = savedJson.TrimEnd(',');
        savedJson += "]}";

        PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "savedObjects", savedJson);
    }

    private String getPositionJson(string type, GameObject gameObject) {
        String result = "{";
        result += "\"type\":" + type + ",";
        result += "\"oil\":" + gameObject.GetComponent<SpriteRenderer>().sortingOrder + ",";
        result += "\"colliderOffsetX\":" + gameObject.GetComponent<BoxCollider2D>().offset.x + ",";
        result += "\"colliderOffsetY\":" + gameObject.GetComponent<BoxCollider2D>().offset.y + ",";
        result += "\"x\":" + gameObject.transform.position.x + ",";
        result += "\"y\":" + gameObject.transform.position.y;
        result += "},";
        return result;
    }

    private void LoadState() {
        if (PlayerPrefs.HasKey("lastLevel")) {
            Debug.Log(PlayerPrefs.GetString("lastLevel"));
            if (PlayerPrefs.GetString("lastLevel") == "Level_2_Part2") {
                gameObject.transform.position = new Vector3(48.52f, -12.14f, 0);
                PlayerPrefs.DeleteKey("lastLevel");
            }
            if (PlayerPrefs.GetString("lastLevel") == "Level_1_Part2") {
                gameObject.transform.position = new Vector3(-11.72f, -14.69f, 0);
                PlayerPrefs.DeleteKey("lastLevel");
            }
        }

        if (PlayerPrefs.HasKey("babySalatsCollected")) {
            babySalatsCollected = PlayerPrefs.GetInt("babySalatsCollected");
            PlayerPrefs.DeleteKey("babySalatsCollected");
        }
        if (PlayerPrefs.HasKey("greenKeyCollected")) {
            greenKeyCollected = PlayerPrefs.GetInt("greenKeyCollected");
            if (greenKeyCollected > 0) {
                GameObject key = GameObject.FindWithTag("greenKey");
                if (key != null) {
                    Destroy(key);
                }
            }
            PlayerPrefs.DeleteKey("greenKeyCollected");
        }
        if (PlayerPrefs.HasKey("yellowKeyCollected")) {
            yellowKeyCollected = PlayerPrefs.GetInt("yellowKeyCollected");
            if (yellowKeyCollected > 0) {
                GameObject key = GameObject.FindWithTag("yellowKey");
                if (key != null) {
                    Destroy(key);
                }
            }
            PlayerPrefs.DeleteKey("yellowKeyCollected");
        }
        if (PlayerPrefs.HasKey("blueKeyCollected")) {
            blueKeyCollected = PlayerPrefs.GetInt("blueKeyCollected");
            if (blueKeyCollected > 0) {
                GameObject key = GameObject.FindWithTag("blueKey");
                if (key != null) {
                    Destroy(key);
                }
            }
            PlayerPrefs.DeleteKey("blueKeyCollected");
        }
        if (PlayerPrefs.HasKey("redKeyCollected")) {
            redKeyCollected = PlayerPrefs.GetInt("redKeyCollected");
            if (redKeyCollected > 0) {
                GameObject key = GameObject.FindWithTag("redKey");
                if (key != null) {
                    Destroy(key);
                }
            }
            PlayerPrefs.DeleteKey("redKeyCollected");
        }
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "savedObjects")) {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
                Destroy(enemy);
            }
            foreach (GameObject salad in GameObject.FindGameObjectsWithTag("Baby_salat")) {
                Destroy(salad);
            }

            string json = PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "savedObjects");
            PositionArray positions = JsonUtility.FromJson<PositionArray>(json);
            foreach (Position position in positions.positions) {
                GameObject tempObject = null;
                if (position.type == "salad") {
                    tempObject = (GameObject) Instantiate(Resources.Load("baby_salat"));
                }
                else {
                    tempObject = (GameObject) Instantiate(Resources.Load("Enemy"));
                }
                tempObject.transform.position = new Vector3(position.x, position.y);
                tempObject.GetComponent<SpriteRenderer>().sortingOrder = position.oil;
                tempObject.GetComponent<BoxCollider2D>().offset =
                    new Vector2(position.colliderOffsetX, position.colliderOffsetY);
            }
            PlayerPrefs.DeleteKey(SceneManager.GetActiveScene().name + "savedObjects");
        }
    }

    private void UpdateHUD() {
        GameObject.FindGameObjectWithTag("Baby_salat_text").GetComponent<Text>().text = babySalatsCollected + "/6";
        GameObject.FindGameObjectWithTag("Yellow_Key_Text").GetComponent<Text>().text = yellowKeyCollected + "/1";
        GameObject.FindGameObjectWithTag("Green_Key_Text").GetComponent<Text>().text = greenKeyCollected + "/1";
    }

    [Serializable]
    public class Position{
        public string type;
        public int oil;
        public float x;
        public float y;
        public float colliderOffsetX;
        public float colliderOffsetY;

        public Position(string type, int oil, float x, float y, float colliderOffsetX, float colliderOffsetY) {
            this.type = type;
            this.oil = oil;
            this.x = x;
            this.y = y;
            this.colliderOffsetX = colliderOffsetX;
            this.colliderOffsetY = colliderOffsetY;
        }

        public override string ToString() {
            return type + ", " + x + ", " + y;
        }
    }

    [Serializable]
    public class PositionArray{
        public Position[] positions;

        public PositionArray(Position[] positions) {
            this.positions = positions;
        }
    }
}