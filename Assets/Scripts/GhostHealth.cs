using Pathfinding;
using UnityEngine;

public class GhostHealth : MonoBehaviour
{
    public bool alive = true;
    public float suckSpeed = 3.0f;

    private Transform shootFromPoint;

    private float killedAtTime;
    private float endKillTime;
    private float killedAtScale;

    [Header("Points")]
    public int pointsPerGhost = 1000;
    public PointsPopUp pointsPopupPrefab;
    public Vector3 popupOffset = new Vector3(0f, 0.2f, 0f); 

    [Header("Popup Rendering")]
    public float popupZ = 0f;            
    public float popupWorldScale = 0.05f;
    public int popupSortingOrder = 2000;  

    private PlayerStats playerStats;
    private Transform visual;

    // Makes sure we only award/spawn once
    private bool awardedPoints = false;

    void Start()
    {
        visual = transform.Find("Visual");

        GameObject playerObj = GameObject.Find("Player");
        if (playerObj == null)
        {
            Debug.LogError("GhostHealth: Could not find GameObject named 'Player'.");
            return;
        }

        shootFromPoint = playerObj.transform.Find("Weapon/FirePoint");
        if (shootFromPoint == null)
        {
            Debug.LogError("GhostHealth: FirePoint not found! Expected Player > Weapon > FirePoint.");
        }

        playerStats = playerObj.GetComponent<PlayerStats>();
        if (playerStats == null)
        {
            Debug.LogError("GhostHealth: PlayerStats not found on Player.");
        }
    }

    void Update()
    {
        if (!alive && shootFromPoint != null)
        {
            float step = suckSpeed * Time.deltaTime;

            // Move toward the vacuum FirePoint
            transform.position = Vector2.MoveTowards(transform.position, shootFromPoint.position, step);

            float duration = (endKillTime - killedAtTime);
            float progress = duration > 0f ? Mathf.Abs(killedAtTime - Time.time) / duration : 1f;

            // Shrink the visual
            if (visual != null)
            {
                visual.localScale = Vector3.one * Mathf.Lerp(killedAtScale, 0f, progress);
            }

            // When fully sucked in, award points + spawn popup ONCE, then destroy
            if (progress >= 1f)
            {
                AwardPointsAndPopupOnce();
                Destroy(gameObject);
            }
        }
    }

    public void Kill()
    {
        if (!alive) return;

        alive = false;

        // Start suck/shrink timing
        if (shootFromPoint != null)
        {
            float distance = Vector2.Distance(transform.position, shootFromPoint.position);
            killedAtTime = Time.time;
            endKillTime = Time.time + (distance * 0.5f);
        }
        else
        {
            killedAtTime = Time.time;
            endKillTime = Time.time + 0.5f;
        }

        killedAtScale = (visual != null) ? visual.localScale.y : transform.localScale.y;

        // Stop AI movement if present
        AILerp ai = GetComponent<AILerp>();
        if (ai != null) ai.enabled = false;
    }

    private void AwardPointsAndPopupOnce()
    {
        if (awardedPoints) return;
        awardedPoints = true;

        // Award points
        if (playerStats != null)
            playerStats.AddScore(pointsPerGhost);

        // Spawn popup at the gun's FirePoint (right when the ghost finishes being sucked)
        if (pointsPopupPrefab != null && shootFromPoint != null)
        {
            Vector3 spawnPos = shootFromPoint.position + popupOffset;
            spawnPos.z = popupZ;

            PointsPopUp popup = Instantiate(pointsPopupPrefab, spawnPos, Quaternion.identity);
            popup.Setup(pointsPerGhost);

            // Force scale (prevents tiny prefab scale issues)
            popup.transform.localScale = Vector3.one * popupWorldScale;

            // Force draw order on top
            Canvas canvas = popup.GetComponentInChildren<Canvas>(true);
            if (canvas != null)
            {
                canvas.renderMode = RenderMode.WorldSpace;
                canvas.overrideSorting = true;
                canvas.sortingOrder = popupSortingOrder;
            }
        }
    }
}
