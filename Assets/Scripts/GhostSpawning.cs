using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GhostSpawning : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float ghostsSpawned = 0;

    public GameObject ghostPrefab;
    public float minGhostSpeed;
    public float maxGhostSpeed;
    public AudioClip[] ghostSpawnAudio;
    public AudioSource audioSource;

    [Header("Intervals for spawn rates")]

    public float initialInterval = 2f;   // Start spawning every 5 seconds
    public float minInterval = 0.2f;     // Never spawn faster than every 0.5 seconds

    [Header("Curve Tuning")]
    public float steepness = 0.2f;       // k: How fast the gap closes
    public float midpoint = 70f;        // x0: Point of fastest change (e.g., at 60s)

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GameObject furnitureItem = PickFurniture();
            if (ghostsSpawned == 0)
            {
                furnitureItem = GameObject.Find("FirstGhostSpawn");
            }
            StartCoroutine(SpawnGhost(furnitureItem));
            // Reset timer using the logistic curve based on total game time
            timer = CalculateCurrentInterval(Time.timeSinceLevelLoad);
        }
    }

    float CalculateCurrentInterval(float timeElapsed)
    {
        float range = initialInterval - minInterval;
        // Logistic growth part (0 to 1)
        float logisticGrowth = 1f / (1.0f + Mathf.Exp(-steepness * (timeElapsed - midpoint)));

        // Subtract growth from initial to get a decreasing value
        return initialInterval - (range * logisticGrowth);
    }

    void Start()
    {
    }


    GameObject PickFurniture()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Furniture");

        int randomIndex = Random.Range(0, objectsWithTag.Length);

        return objectsWithTag[randomIndex];
    }

    IEnumerator SpawnGhost(GameObject furnitureItem)
    {

        furnitureItem.GetComponent<Animator>().SetTrigger("PossessFurniture");
        yield return new WaitForSeconds(2);
        var ghost = GameObject.Instantiate(ghostPrefab, furnitureItem.transform.position, furnitureItem.transform.rotation);
        ghost.GetComponent<AILerp>().speed = Random.Range(minGhostSpeed, maxGhostSpeed);

        audioSource.PlayOneShot(
            ghostSpawnAudio[Random.Range(0, ghostSpawnAudio.Length)]);

        ghostsSpawned++;

        yield return null;
    }

  
}
