using Pathfinding;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GhostHealth : MonoBehaviour
{
    public bool alive = true;
    public float suckSpeed = 3.0f;
    public Transform shootFromPoint;
    public Transform player;
    private float killedAtTime;
    private float endKillTime;
    private float killedAtScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootFromPoint = GameObject.Find("Player").transform.Find("Weapon").Find("FirePoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive && shootFromPoint != null)
        {
            float step = suckSpeed * Time.deltaTime;

            // Move the object towards the target position
            transform.position = Vector2.MoveTowards(transform.position, shootFromPoint.position, step);
            

            float progress = Mathf.Abs(killedAtTime-Time.time) / (endKillTime-killedAtTime);
            //Debug.Log("TIME IS " + Mathf.Abs(killedAtTime - Time.time) + " DIVIDED BY  " + (endKillTime - killedAtTime));
            transform.Find("Visual").transform.localScale = Vector3.one * Mathf.Lerp(killedAtScale, 0, progress);

            if (progress >= 1f)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    public void Kill()
    {
        if (alive)
        {
            float distance = Vector2.Distance(transform.position, shootFromPoint.position);

            alive = false;
            killedAtTime = Time.time;
            endKillTime = Time.time + (distance*.5f);
            killedAtScale = transform.localScale.y;
            GetComponent<AILerp>().enabled = false;
        }
    }   
}
