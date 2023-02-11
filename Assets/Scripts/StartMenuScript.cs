using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartMenuScript : MonoBehaviour
{

    public Text highScoreText;

    private void Start()
    {

        if (PlayerPrefs.GetString("HIGHSCORENAME") != "")
        {
        highScoreText.text = "High Score " + PlayerPrefs.GetInt("HIGHSCORE") + "\n" +
            "Set By " + PlayerPrefs.GetString("HIGHSCORENAME");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game Pushed");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");

    }

}
