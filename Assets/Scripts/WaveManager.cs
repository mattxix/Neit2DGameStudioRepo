using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    int waveNumber = 0;
    int enemiesPerWave = 0;
    int zombiesKilled = 0;
    public bool isWaveActive = true;
    public Canvas WaveCanvas;
    public TMP_Text waveText;
    public Button StartNight;

    void Start()
    {
        StartNight.gameObject.SetActive(false);
        isWaveActive = true;
        waveNumber = 0;
        enemiesPerWave = 5;
        zombiesKilled = 0;
        if (waveText != null)
        {
            waveText.gameObject.SetActive(false);
        }
        StartWave();
    }

    void StartWave()  
    {
        if (isWaveActive)
        {
            waveNumber++;
            zombiesKilled = 0;
            // Only increment after the first wave
            if (waveNumber > 1)
                enemiesPerWave += 5;
            StartCoroutine(DisplayWaveTextAndSpawn());
        }
    }

    IEnumerator DisplayWaveTextAndSpawn()
    {
        if (waveText != null)
        {
            waveText.text = "Wave " + waveNumber;
            waveText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(4f); 

        if (waveText != null)
        {
            waveText.gameObject.SetActive(false);
        }
        // Start spawning zombies for this wave
        FindObjectOfType<ZombieSpawner>()?.StartNewWave(enemiesPerWave);
    }

    IEnumerator EndWave()
    {
        Debug.Log("Wave " + waveNumber + " Survived");
        isWaveActive = false;
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
        FindObjectOfType<ZombieSpawner>()?.StopSpawning();
        PeaceTime();
    }

    private void PeaceTime()
    {
        waveText.gameObject.SetActive(true);
        StartNight.gameObject.SetActive(true);
        waveText.text = "Peacetime ";
    }

    public void OnZombieKilled()
    {
        zombiesKilled++;
        if (zombiesKilled >= enemiesPerWave && isWaveActive)
        {
            StartCoroutine(EndWave());
        }
    }
}
