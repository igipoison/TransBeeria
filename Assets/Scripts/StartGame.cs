using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public void StartLevel(int level)
    {
        ////Application.LoadLevel(level);
        SceneManager.LoadScene("main");
    }

    public void StartDevPage()
    {
        ////Application.LoadLevel(level);
        SceneManager.LoadScene("devs");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("menu");
        SceneManager.UnloadScene("devs");
    }

    public void QuitApp()
    {
        SceneManager.UnloadScene("menu");

    }
}
