using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    int waveNumber;
    int enemiesPerWave = 0;
    int zombiesKilled = 0;
    public bool isWaveActive = true;
    public Canvas WaveCanvas;
    public TMP_Text waveText;
    public TMP_Text peaceTimeText;
    public Button StartNight;
    public ZombieSpawner ZombieSpawner;

    void Start()
    {
        StartNight.gameObject.SetActive(false);
        isWaveActive = true;
        waveNumber = 0;
        zombiesKilled = 0;
        if (waveText != null)
        {
            waveText.gameObject.SetActive(false);
        }
        StartWave();
    }

    void StartWave()  
    {
        isWaveActive = true; 
        waveNumber += 1;
        zombiesKilled = 0;
        enemiesPerWave += 5;
        Debug.Log(enemiesPerWave);
        
        StartCoroutine(DisplayWaveTextAndSpawn());
    }

    IEnumerator DisplayWaveTextAndSpawn()
    {
        if (waveText != null)
        {
            waveText.text = "Wave " + waveNumber;
            Debug.Log("WaveStarted");
            waveText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(4f); 

        if (waveText != null)
        {
            waveText.gameObject.SetActive(false);
        }

        ZombieSpawner.StartNewWave(enemiesPerWave);
    }

    IEnumerator EndWave()
    {
        ZombieSpawner.StopSpawning();
        isWaveActive = false;
        Debug.Log("Wave " + waveNumber + " Survived");
        if (waveText != null)
        {
            waveText.text = "Wave " + waveNumber + " Survived ";
            waveText.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(4f);
        if (waveText != null)
        {
            waveText.gameObject.SetActive(false);
        }

        PeaceTime();
    }

    private void PeaceTime()
    {
        Debug.Log("PeaceTime called");
        peaceTimeText.gameObject.SetActive(true);
        peaceTimeText.text = "Peacetime ";
        StartNight.gameObject.SetActive(true);
        
    }

    public void OnZombieKilled()
    {
        Debug.Log("Zombie Killed");
        Debug.Log(zombiesKilled + 1 + " / " + enemiesPerWave);
        zombiesKilled++;
        if (zombiesKilled >= enemiesPerWave && isWaveActive)
        {
            Debug.Log("All Zombies Killed");
            StartCoroutine(EndWave());
        }
    }

    public void OnStartNightButtonPressed()
    {
        peaceTimeText.gameObject.SetActive(false);
        StartNight.gameObject.SetActive(false);
        Debug.Log("Wave Started");
        StartWave();
    }
}
