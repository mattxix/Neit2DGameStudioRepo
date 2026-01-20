using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(destroyAuto());
    }

    IEnumerator destroyAuto()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
