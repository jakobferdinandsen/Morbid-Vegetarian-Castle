using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour{
    public void GameStart() {
        LevelManager.firstLaunch = true;
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGame() {
        Application.Quit();
    }
}