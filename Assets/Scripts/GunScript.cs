using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    public int pelletCount = 5; // Number of bullets per shot
    public float spreadAngle = 15f; // Total spread angle in degrees
    public float fireRate = 0.5f; // Delay between shots in seconds

    private bool canFire = true;

    public void Fire()
    {
        if (!canFire)
            return;

        StartCoroutine(FireWithDelay());
    }

    private IEnumerator FireWithDelay()
    {
        canFire = false;

        float angleStep = spreadAngle / (pelletCount - 1);
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < pelletCount; i++)
        {
            float currentAngle = startAngle + angleStep * i;
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, currentAngle);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(rotation * Vector2.up * fireForce, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}
