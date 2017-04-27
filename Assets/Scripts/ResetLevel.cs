using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{

    private Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void LevelReset()
    {
        Application.LoadLevel(scene.name);
        Time.timeScale = 1;
    }
}