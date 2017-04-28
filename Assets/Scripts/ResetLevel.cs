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

    public void GoToMainMenu() {
        LevelManager.firstLaunch = true;
        Destroy(GameObject.FindWithTag("LevelManager"));
        SceneManager.LoadScene("Main_Menu");
        Time.timeScale = 1;
    }
}