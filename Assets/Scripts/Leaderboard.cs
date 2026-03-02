using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{

    public string password;
    public string enteredName;
    public TMP_Text NameDisplay;
    public int nameLength;
    public Canvas Namepanel;


    void Start()
    {
        nameLength = 3;
        NameDisplay.text = "Enter Name";
    }

    void Update()
    {
        if (enteredName.Length == nameLength)
        {
           Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void EnterName()
    {
        NameDisplay.text = enteredName;
    }
}
