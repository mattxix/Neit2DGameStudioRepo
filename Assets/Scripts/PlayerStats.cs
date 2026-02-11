using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text deathTimerText;
    public TMP_Text scoreText;  

    [Header("State")]
    public bool timerRunning = true;

    private float timeAlive;

    [Header("Score")]
    public int score = 0;

    public const string LAST_TIME_KEY = "LAST_SURVIVAL_TIME";
    public const string BEST_TIME_KEY = "BEST_SURVIVAL_TIME";

    void Start()
    {
        timeAlive = 0f;
        timerRunning = true;

        if (deathTimerText != null) deathTimerText.text = "";

        UpdateScoreUI();
    }

    void Update()
    {
        if (!timerRunning) return;

        timeAlive += Time.deltaTime;

        if (timerText != null)
            timerText.text = "Time: " + FormatTime(timeAlive);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    public void StopAndSave()
    {
        if (!timerRunning) return;

        timerRunning = false;

        PlayerPrefs.SetFloat(LAST_TIME_KEY, timeAlive);

        float best = PlayerPrefs.GetFloat(BEST_TIME_KEY, 0f);
        if (timeAlive > best)
        {
            best = timeAlive;
            PlayerPrefs.SetFloat(BEST_TIME_KEY, best);
        }

        PlayerPrefs.Save();

        if (deathTimerText != null)
            deathTimerText.text = "Survived: " + FormatTime(timeAlive);
    }

    public void ShowSavedTimes()
    {
        float last = PlayerPrefs.GetFloat(LAST_TIME_KEY, 0f);
        float best = PlayerPrefs.GetFloat(BEST_TIME_KEY, 0f);

        if (deathTimerText != null)
            deathTimerText.text = "Survived: " + FormatTime(last) + "\nBest: " + FormatTime(best);
    }

    private string FormatTime(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60f);
        int secs = Mathf.FloorToInt(seconds % 60f);
        return minutes.ToString("00") + ":" + secs.ToString("00");
    }
}
