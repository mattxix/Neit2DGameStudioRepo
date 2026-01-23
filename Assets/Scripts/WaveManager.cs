using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    int waveNumber = 0;
    int enemiesPerWave;
    int zombiesSpawned;
    int zombiesKilled;
    public bool isWaveActive = true;
    public Canvas WaveCanvas;
    public TMP_Text waveText;
    public Button StartNight;

    void Start()
    {
        StartNight.gameObject.SetActive(false);
        isWaveActive = true;
        enemiesPerWave = 5;
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
            enemiesPerWave += 5;
            zombiesKilled = 0;
            zombiesSpawned = 0; 
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

    public void IncrementZombiesSpawned()
    {
        zombiesSpawned++;
    }
}
