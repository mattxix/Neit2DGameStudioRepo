using System.Collections;
using Pathfinding;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int zombieHealth = 100;
    private WaveManager waveManager;
    public Transform player;
    private bool isDead = false;
   // private AIDestinationSetter destinationSetter;

    public void Initialize(WaveManager manager)
    {
        waveManager = manager;
    }

    private void EmitBlood(int damageAmt)
    {
        GetComponent<ParticleSystem>().Emit(damageAmt);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return; // Prevent multiple kills

        zombieHealth -= amount;
        EmitBlood(amount);

        if (zombieHealth <= 0)
        {
            isDead = true; 
            waveManager?.OnZombieKilled(transform.position);
            StartCoroutine(DeathAnimation());
        }
    }

    IEnumerator DeathAnimation()
    {
        GetComponent<AILerp>().enabled = false;
        var rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        rb.linearVelocity = Vector3.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    private int GetDamageAmt()
    {
        var distance = Vector2.Distance(transform.position, player.position) * 8; 
        Debug.Log("DISTANCE: " +  distance);
        if (distance > 0 && distance <= 1)
        {
            return 30;
        }
        else if(distance > 1 && distance <= 3)
        {
            return 17;
        }
        else if (distance > 3 && distance <= 8)
        {
            return 12;
        }
        else if (distance > 8)
        {
            return 5;
        }

        return 0;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(GetDamageAmt());
            Destroy(other.gameObject);
        }
    }
}
