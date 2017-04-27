using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour{
    public static bool firstLaunch = true;

    void Awake() {
        if (firstLaunch) {
            DontDestroyOnLoad(gameObject);
            firstLaunch = false;
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("currentLevel", SceneManager.GetActiveScene().name);
        }
    }
}