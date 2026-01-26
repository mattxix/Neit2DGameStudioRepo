using Pathfinding;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public Transform playerTransform; 
    public float spawnInterval = 1.0f;
    private int zombiesPerWave = 0;
    private float timer;
    private int zombiesSpawnedThisWave;
    private bool spawnZombies = false;
    private WaveManager waveManager;

    
    void Start()
    {
        timer = 0f;
        zombiesSpawnedThisWave = 0;
    }

    void Update()
    {
        if (!spawnZombies || spawnPoints.Length == 0)
        { 
        return;
        }

        timer += Time.deltaTime;
        if (timer >= spawnInterval && zombiesSpawnedThisWave < zombiesPerWave)
        {
            SpawnZombieAtUniquePoint();
            timer = 0f;
        }
    }

    void Awake()
    {
        if (waveManager == null)
            waveManager = FindFirstObjectByType<WaveManager>();
    }

    void SpawnZombieAtUniquePoint()
    {
        int spawnIndex = zombiesSpawnedThisWave % spawnPoints.Length;
        Transform spawnPoint = spawnPoints[spawnIndex];

        Vector3 offset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0f);
        GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position + offset, spawnPoint.rotation);


        ZombieHealth zombieScript = zombie.GetComponent<ZombieHealth>();
        if (zombieScript == null)
        {
            Debug.LogError("Zombie prefab is missing Zombie script!");
            return;
        }
        zombieScript.Initialize(waveManager);

        AIDestinationSetter followScript = zombie.GetComponent<AIDestinationSetter>();
        if (followScript != null && playerTransform != null)
        {
            followScript.target = playerTransform;
        }



        zombiesSpawnedThisWave++;
        Debug.Log("Spawned zombie " + zombiesSpawnedThisWave + " at " + spawnPoint.position);
    }

    public void StartNewWave(int zombieCount)
    {
        spawnZombies = true;
        Debug.Log("Starting new wave with " + zombieCount + " zombies.");
        zombiesPerWave = zombieCount;
        zombiesSpawnedThisWave = 0;
        timer = 0f;
        Debug.Log("Zombies spawned this wave: " + zombiesSpawnedThisWave);
    }

    public void StopSpawning()
    {
        spawnZombies = false;
    }
}
