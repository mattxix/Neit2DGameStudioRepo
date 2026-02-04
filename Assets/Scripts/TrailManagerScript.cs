using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrailManagerScript : MonoBehaviour
{
    public Tilemap map;
    public TileBase trailTile;
    public float trailTime = 5f;

    public GameObject player;
    public float slowMultiplier = 0.5f;

    private HashSet<Vector3Int> activeTrailCells = new HashSet<Vector3Int>();
    private PlayerScript playerMove;

    void Awake()
    {
        if (player != null)
            playerMove = player.GetComponent<PlayerScript>();
    }

    void Update()
    {
        // 1) Place trail tiles under ghosts
        foreach (GameObject ghost in GameObject.FindGameObjectsWithTag("Ghost"))
        {
            var gh = ghost.GetComponent<GhostHealth>();
            if (gh != null && gh.alive)
            {
                Vector3Int cell = map.WorldToCell(ghost.transform.position);
                cell.z = 0; // IMPORTANT

                if (!activeTrailCells.Contains(cell))
                {
                    activeTrailCells.Add(cell);
                    map.SetTile(cell, trailTile);
                    StartCoroutine(ResetTileToNormal(cell));
                }
            }
        }

        // 2) Slow player if the TRAIL TILE is actually under them
        if (playerMove != null && player != null)
        {
            Vector3Int playerCell = map.WorldToCell(player.transform.position);
            playerCell.z = 0; // IMPORTANT

            TileBase tileUnderPlayer = map.GetTile(playerCell);

            bool slowed = (tileUnderPlayer == trailTile);

            playerMove.SetSpeedMultiplier(slowed ? slowMultiplier : 1f);

            // Optional debug (delete later)
            // Debug.Log($"PlayerCell={playerCell} tileUnder={tileUnderPlayer} trailTile={trailTile} slowed={slowed}");
        }
    }

    IEnumerator ResetTileToNormal(Vector3Int cell)
    {
        yield return new WaitForSeconds(trailTime);

        map.SetTile(cell, null);
        activeTrailCells.Remove(cell);
    }
}
