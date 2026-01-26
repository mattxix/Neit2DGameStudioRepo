using UnityEngine;
using Pathfinding;

public class ZombieGFX : MonoBehaviour
{
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 delta = transform.position - lastPosition;

        if (delta.x > 0.001f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (delta.x < -0.001f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        lastPosition = transform.position;
    }

}
