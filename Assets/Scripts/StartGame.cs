using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    private static int currentLevel = 1; 

    public void StartLevel(int level)
    {
        ////Application.LoadLevel(level);
        //if (level > 1)
        //{
        //    SceneManager.UnloadScene("main");
        //}
        SceneManager.LoadScene("main");
    }

    public static int getLevel()
    {
        return currentLevel;
    }

    public void StartDevPage()
    {
        ////Application.LoadLevel(level);
        SceneManager.LoadScene("devs");
    }

    public void StartNewLevel()
    {
        Debug.Log("new level called");
        currentLevel++; 
        if (currentLevel > 1)
        {
           SceneManager.UnloadScene("main");
        }
        ////Application.LoadLevel(level);
        SceneManager.LoadScene("main");
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
