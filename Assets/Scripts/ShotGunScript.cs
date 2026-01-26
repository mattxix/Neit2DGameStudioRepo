using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class ShotGunScript : MonoBehaviour
{
    public GameObject bulletPrefab2;
    public Transform firePoint2;
    public float fireForce2 = 12f;
    public int pelletCount2 = 10; // Number of bullets per shot
    public float spreadAngle2 = 15f; // Total spread angle in degrees
    public float fireRate2 = 0.5f; // Delay between shots in seconds

    private bool shotGunCanFire = true;

    public void Fire()
    {
        if (!shotGunCanFire)
            return;

        StartCoroutine(ShotGunFireWithDelay());
    }

    private IEnumerator ShotGunFireWithDelay()
    {
        shotGunCanFire = false;

        float angleStep = spreadAngle2 / (pelletCount2 - 1); 
        float startAngle = -spreadAngle2 / 2f;               

        for (int i = 0; i < pelletCount2; i++)
        {
            // Centered angle + small random deviation if desired
            float angle = startAngle + angleStep * i;
            // Optional: small random offset to make spread less perfect
            float randomOffset = Random.Range(-angleStep / 2f, angleStep / 2f);
            angle += randomOffset;

            Quaternion rotation = firePoint2.rotation * Quaternion.Euler(0, 0, angle);

            GameObject bullet = Instantiate(bulletPrefab2, firePoint2.position, rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(rotation * Vector2.up * Random.Range(2f, fireForce2), ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(fireRate2);
        shotGunCanFire = true;
    }

}
