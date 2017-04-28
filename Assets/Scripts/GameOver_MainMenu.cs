using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver_MainMenu : MonoBehaviour {

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
