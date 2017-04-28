using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour{
    private int health = 3;

    void Awake() {
        LoadState();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBullet")) {
            if (--health == 0) {
                //TODO Show game over screen
            }
            UpdateHUD();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("BigGuy")) {
            //TODO Show game over screen
        }
    }

    public void SaveState() {
        PlayerPrefs.SetInt("health", health);
    }

    private void LoadState() {
        if (PlayerPrefs.HasKey("health")) {
            health = PlayerPrefs.GetInt("health");
            UpdateHUD();
            PlayerPrefs.DeleteKey("health");
        }
    }

    private void UpdateHUD() {
        GameObject.FindWithTag("Health1").GetComponent<SpriteRenderer>().enabled = health > 0;
        GameObject.FindWithTag("Health2").GetComponent<SpriteRenderer>().enabled = health > 1;
        GameObject.FindWithTag("Health3").GetComponent<SpriteRenderer>().enabled = health > 2;
    }
}