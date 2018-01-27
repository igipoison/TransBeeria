using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public void StartLevel(int level)
    {
        ////Application.LoadLevel(level);
        SceneManager.LoadScene("Main");
    }
}
