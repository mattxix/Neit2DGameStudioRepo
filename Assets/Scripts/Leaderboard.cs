using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = "3e1e098c93083a73460169c5ce32989c999a5d5132358e4e68995085115bd0e5";

    private void Start()
    {
        LeaderboardCreator.RequestUserGuid(guid =>
        {
            LeaderboardCreator.SetUserGuid(guid);
            GetLeaderboard();
        });
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, msg =>
        {
            int loopLength = Mathf.Min(msg.Length, names.Count);
            for (int i = 0; i < loopLength; i++)
            {
              names[i].text = msg[i].Username;
              scores[i].text = msg[i].Score.ToString();
            }
        });
    }


    [SerializeField] private GameObject loadingText;
    [SerializeField] private UnityEngine.UI.Button submitButton;

    public void SetLeaderboardEntry(string username, int score)
    {
        loadingText.SetActive(true);
        // Disable button immediately
        submitButton.interactable = false;

        // Clean username
        username = (username ?? "").Trim();

        if (username.Length == 0)
            username = "Anon";

        if (username.Length > 12)
            username = username.Substring(0, 12);

        if (score < 0)
            score = 0;

        LeaderboardCreator.UploadNewEntry(
            publicLeaderboardKey,
            username,
            score,
            isSuccessful =>
            {
                if (isSuccessful)
                loadingText.SetActive(false);
                GetLeaderboard();

                // Re-enable button after response
                submitButton.interactable = true;
                Debug.Log($"Uploading entry: username='{username}', score={score}");
            },
            err =>
            {
                Debug.LogError("Upload error: " + err);

                // Re-enable button even if it failed
                submitButton.interactable = true;
            });
    }
}
