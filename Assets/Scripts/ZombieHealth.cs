using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int zombieHealth = 3;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
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
}
