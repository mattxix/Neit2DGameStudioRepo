using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("WallTriggers"))
        {
            Debug.Log("Specifically hit the object with the correct tag!");
            Destroy(gameObject);
            // Add your custom logic here (e.g., collect item, open door)
        }
    }




    private void Start()
    {
        StartCoroutine(destroyAuto());
    }

    public IEnumerator destroyAuto()
    {
        yield return new WaitForSeconds(1.25f);
        Destroy(gameObject);

    }
}
