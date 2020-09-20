using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{

    public Text text;
    Player pl;
    public GameObject DeathPanel;
    public GameObject Joystick;
    public GameObject FireButton;
    private void Start()
    {
        pl = Player.singleton;
    }

    void Update()
    {
        if (pl.Life >= 0)
        {
            text.text = pl.Life.ToString();
        }
        else
        {
            text.text = "0";
        }
        if(pl.Life <= 0)
        {
            Joystick.SetActive(false);
            FireButton.SetActive(false);
            DeathPanel.SetActive(true);
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
