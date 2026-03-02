using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI inputScore;
    [SerializeField]
    private TMP_InputField inputName;

    public UnityEvent<string, int> submitScoreEvent;
    public void SubmitScore()
    {
        
        string rawName = inputName.text;
        string rawScore = inputScore.text;

        string cleanedScore = rawScore.Trim()
                                      .Replace(",", "")
                                      .Replace("Score:", "")
                                      .Replace("score:", "")
                                      .Trim();

        if (!int.TryParse(cleanedScore, out int parsedScore))
        {
            Debug.LogError($"Score parse failed. rawScore='{rawScore}' cleanedScore='{cleanedScore}'");
            return;
        }

        Debug.Log($"About to submit. name='{rawName}' rawScore='{rawScore}' parsedScore={parsedScore}");
        submitScoreEvent.Invoke(rawName, parsedScore);
    }
}
