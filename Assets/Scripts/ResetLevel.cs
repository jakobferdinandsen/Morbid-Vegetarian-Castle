using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour{
    public void LevelReset() {
        if (PlayerPrefs.HasKey("currentLevel")) {
            SceneManager.LoadScene(PlayerPrefs.GetString("currentLevel"));
            Time.timeScale = 1;
        }
    }
}