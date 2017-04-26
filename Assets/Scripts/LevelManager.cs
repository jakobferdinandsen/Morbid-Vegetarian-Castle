using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour{
    public static bool firstLaunch = true;

    void Awake() {
        if (firstLaunch) {
            DontDestroyOnLoad(gameObject);
            firstLaunch = false;
            PlayerPrefs.DeleteAll();
        }
    }
}