using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public GameObject statsScreen;
    public GameObject gameOverTitle;
    public GameObject buttons;
   
    public void Start()
    {
        statsScreen.SetActive(false);
        gameOverTitle.SetActive(false);
        buttons.SetActive(false);
        
        
    }
    public void GameOverScreen()
    {
        statsScreen.SetActive(true);
        gameOverTitle.SetActive(true);
        buttons.SetActive(true);
    }
}
