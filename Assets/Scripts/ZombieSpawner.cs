using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public Transform playerTransform; // Assign in Inspector
    public bool spawnZombies = true;
    public float spawnInterval = 5f;
    public int zombiesPerWave = 5;
    private float timer;
    private int zombiesSpawnedThisWave;

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
}
