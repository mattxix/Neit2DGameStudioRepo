using UnityEngine;
using Pathfinding;

public class ZombieGFX : MonoBehaviour
{
    private Vector3 lastPosition;

    void Start()
    {
    }

    void Update()
    {
        lastPosition = GameObject.Find("Player").transform.position;

        Vector3 delta = transform.position - lastPosition;

        GetComponent<SpriteRenderer>().flipX = (delta.x > 0);

       // lastPosition = transform.position;
    }

}
