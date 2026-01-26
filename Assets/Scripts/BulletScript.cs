using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet collsion");
        Destroy(gameObject);
    }


    private void Start()
    {
        StartCoroutine(destroyAuto());
    }

    public IEnumerator destroyAuto()
    {
        yield return new WaitForSeconds(1.25f);
        Debug.Log("Bullet Destroyed");
        Destroy(gameObject);

    }
}
