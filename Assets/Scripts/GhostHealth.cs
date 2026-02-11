using Pathfinding;
using UnityEngine;

public class GhostHealth : MonoBehaviour
{
    public bool alive = true;
    public float suckSpeed = 3.0f;
    public Transform shootFromPoint;

    private float killedAtTime;
    private float endKillTime;
    private float killedAtScale;

    [Header("Points")]
    public int pointsPerGhost = 1000;
    public PointsPopUp pointsPopupPrefab;  
    public Vector3 popupOffset = new Vector3(0f, 1f, 0f);

    private PlayerStats playerStats;

    void Start()
    {
        shootFromPoint = GameObject.Find("Player").transform.Find("Weapon").Find("FirePoint");

        // find PlayerStats once
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (!alive && shootFromPoint != null)
        {
            float step = suckSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, shootFromPoint.position, step);

            float progress = Mathf.Abs(killedAtTime - Time.time) / (endKillTime - killedAtTime);

            transform.Find("Visual").transform.localScale =
                Vector3.one * Mathf.Lerp(killedAtScale, 0, progress);

            if (progress >= 1f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Kill()
    {

        if (!alive) return;

        alive = false;

        // Award points ONCE, right when kill starts
        if (playerStats != null)
            playerStats.AddScore(pointsPerGhost);

        // Spawn popup
        if (pointsPopupPrefab != null)
        {
            Debug.Log("Spawning popup at: " + (transform.position + popupOffset));

            Vector3 spawnPos = transform.position + popupOffset;
            PointsPopUp popup = Instantiate(pointsPopupPrefab, spawnPos, Quaternion.identity);
            popup.Setup(pointsPerGhost);
        }

        float distance = Vector2.Distance(transform.position, shootFromPoint.position);
        killedAtTime = Time.time;
        endKillTime = Time.time + (distance * .5f);
        killedAtScale = transform.localScale.y;

        GetComponent<AILerp>().enabled = false;
    }
}
