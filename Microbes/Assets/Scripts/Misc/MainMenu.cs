using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void LoadSingleplayer()
    {
        SceneManager.LoadScene("SinglePlayerTest");
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
