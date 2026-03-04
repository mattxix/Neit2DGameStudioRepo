using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject LeaderBoard;

    public void OpenLeaderBoard()
    {
       
       LeaderBoard.SetActive(true);
     
    }

    public void CloseLeaderBoard()
    {

        LeaderBoard.SetActive(false);

    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
