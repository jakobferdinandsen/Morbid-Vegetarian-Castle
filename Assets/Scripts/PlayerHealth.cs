using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour{
    public int health = 3;

    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyProjectile")) {
            health--;
            GameObject[] healthObjects = GameObject.FindGameObjectsWithTag("Health");
            Destroy(healthObjects[healthObjects.Length - 1]);
            Destroy(other.gameObject);
            if (health == 0) {
                //TODO Show game over screen
            }
        }
    }
}