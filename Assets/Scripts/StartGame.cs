using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public void gameStart()
    {
        SceneManager.LoadScene("level_1");
    }

    public void gameOver()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
