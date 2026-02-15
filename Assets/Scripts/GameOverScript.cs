using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [Header("Objects")]
    public GameObject statsScreen;
    public GameObject gameOverTitle;
    public GameObject buttons;
    public GameObject timer;
    public GameObject health;
    public GameObject suckBar;
    public GameObject cursorCanvas;
    public GameObject score;

    
    


    public void Start()
    {
        statsScreen.SetActive(false);
        gameOverTitle.SetActive(false);
        buttons.SetActive(false);
        suckBar.SetActive(true);
        health.SetActive(true);
        timer.SetActive(true);

    }
    public void GameOverScreen()
    {
        Cursor.visible = true;
        cursorCanvas.SetActive(false);
        statsScreen.SetActive(true);
        gameOverTitle.SetActive(true);
        buttons.SetActive(true);
        suckBar.SetActive(false);
        health.SetActive(false);
        timer.SetActive(false);
        score.SetActive(false);

        //**LAST THING NOTHING GOES BENEATH THIS LINE OF CODE**
        Time.timeScale = 0f;
        //------------------------------------------------
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
