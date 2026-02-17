using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GhostSpawning : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float difficulty = .001f;
    private float ghostsSpawned = 0;
    public float falloff = 0.002f;
    public float capIntensityAtSpawned = 40;
    public float minSpawnRate = 1;
    public float randomMult = .5f;
    public float simulationSpeed = 1.0f;
    public GameObject ghostPrefab;
    public float minGhostSpeed;
    public float maxGhostSpeed;
    public AudioClip[] ghostSpawnAudio;
    public AudioSource audioSource;



    void Start()
    {
        StartCoroutine(SpawnGhosts());
    }

    // Update is called once per frame
    void Update()
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

    IEnumerator SpawnGhosts()
    {
        while (true)
        {

            float randomValue = (falloff) * Mathf.Pow(ghostsSpawned - capIntensityAtSpawned, 2) + minSpawnRate;
            if(ghostsSpawned >= capIntensityAtSpawned)
            {
                randomValue = minSpawnRate;
            }
          //  Debug.Log(randomValue);

            randomValue = randomValue * Random.Range(1 - randomMult, 1 + randomMult);
            GameObject furnitureItem = PickFurniture();

            if (ghostsSpawned == 0)
            {
                furnitureItem = GameObject.Find("FirstGhostSpawn");
            }

            StartCoroutine(SpawnGhost(furnitureItem));
            yield return new WaitForSeconds(randomValue);
        }

    }
}
