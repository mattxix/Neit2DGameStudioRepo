using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text timerText;         
    public TMP_Text timerText2;         
    public TMP_Text deathTimerText;  
    public TMP_Text deathScoreText;  
    public TMP_Text deathGhostText;  
    public TMP_Text scoreText;
    
    [Header("State")]
    public bool timerRunning = true;
    public bool flag = true;
    private float timeAlive;

    [Header("Score")]
    public int score = 0;

    public const string LAST_TIME_KEY = "LAST_SURVIVAL_TIME";
    public const string BEST_TIME_KEY = "BEST_SURVIVAL_TIME";

    [Header("Stats")]
    public int ghostKilled = 0;

    [Header("Scripts")]
    public PlayerScript PlayerScript;

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
        {
            timerText.text = FormatTime(timeAlive);
            timerText2.text = FormatTime(timeAlive);
        }   
            timerText.text = FormatTime(timeAlive);
        if (flag)
        StartCoroutine(IncreaseScoreOverTime());
    }

    IEnumerator IncreaseScoreOverTime()
    {
        flag = false;
        if (timeAlive > 0f && timeAlive < 20f)
        {
            yield return new WaitForSeconds(0.1f);
            score += 1;
        }
        else if (timeAlive > 20f && timeAlive < 40f)
        {
            yield return new WaitForSeconds(0.1f);
            score += 2;
        }
        else if (timeAlive > 40f && timeAlive < 60f)
        {
            yield return new WaitForSeconds(0.1f);
            score += 3;
        }
        else if (timeAlive > 60f && timeAlive < 120f)
        {
            yield return new WaitForSeconds(0.1f);
            score += 4;
        }
        else if (timeAlive > 120f )
        {
            yield return new WaitForSeconds(0.1f);
            score += 5;
        }
        flag = true;
        UpdateScoreUI();
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
            deathTimerText.text = FormatTime(timeAlive);
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

    public void OnDeath()
    {
        deathTimerText.text = FormatTime(timeAlive);
        deathScoreText.text = score.ToString();
        deathGhostText.text = ghostKilled.ToString();
    }

    public void GhostSucked()
    {
        ghostKilled++;
    }
}
