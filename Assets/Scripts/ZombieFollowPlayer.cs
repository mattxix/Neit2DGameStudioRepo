using UnityEngine;
using UnityEngine.AI;

public class ZombieFollowPlayer : MonoBehaviour
{ 
    [SerializeField] private Transform target; 
    private NavMeshAgent agent;
    public int zombieHealth = 3;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void TakeDamage(int amount)
    {
        zombieHealth -= amount;
        if (zombieHealth <= 0)
        {
            var waveManager = FindObjectOfType<WaveManager>();
            if (waveManager != null)
            {
                waveManager.OnZombieKilled();
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
