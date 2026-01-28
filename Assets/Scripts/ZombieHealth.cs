using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int zombieHealth = 3;
    private WaveManager waveManager;
    private bool isDead = false;

    public void Initialize(WaveManager manager)
    {
        waveManager = manager;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return; // Prevent multiple kills

        zombieHealth -= amount;

        if (zombieHealth <= 0)
        {
            isDead = true; 
            waveManager?.OnZombieKilled(transform.position);
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
}
