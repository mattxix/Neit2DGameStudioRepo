using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public Transform playerTransform; // Assign in Inspector
    public float spawnInterval = 1.0f;
    private int zombiesPerWave = 0;
    private float timer;
    private int zombiesSpawnedThisWave;
    private bool spawnZombies = false;

    void Start()
    {
        timer = 0f;
        zombiesSpawnedThisWave = 0;
    }

    void Update()
    {
        if (!spawnZombies || spawnPoints.Length == 0)
            return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval && zombiesSpawnedThisWave < zombiesPerWave)
        {
            SpawnZombieAtUniquePoint();
            timer = 0f;
        }
    }

    void SpawnZombieAtUniquePoint()
    {
        int spawnIndex = zombiesSpawnedThisWave % spawnPoints.Length;
        Transform spawnPoint = spawnPoints[spawnIndex];

        Vector3 offset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0f);
        GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position + offset, spawnPoint.rotation);

        ZombieFollowPlayer followScript = zombie.GetComponent<ZombieFollowPlayer>();
        if (followScript != null && playerTransform != null)
        {
            followScript.SetTarget(playerTransform);
        }

        zombiesSpawnedThisWave++;
    }

    public void StartNewWave(int zombieCount)
    {
        zombiesPerWave = zombieCount;
        zombiesSpawnedThisWave = 0;
        timer = 0f;
        spawnZombies = true;
    }

    public void StopSpawning()
    {
        spawnZombies = false;
    }
}
