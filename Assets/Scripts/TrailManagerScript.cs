using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrailManagerScript : MonoBehaviour
{
    public Tilemap map;
    public TileBase trailTile;
    public float trailTime = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject ghost in GameObject.FindGameObjectsWithTag("Ghost"))
        {
            Vector3Int cellPosition = map.WorldToCell(ghost.transform.position);
            if (ghost.GetComponent<GhostHealth>().alive)
            {
                map.SetTile(cellPosition, trailTile);
                StartCoroutine(ResetTileToNormal(cellPosition));
            }

        }
    }

    IEnumerator ResetTileToNormal(Vector3Int pos)
    {
        yield return new WaitForSeconds(trailTime);
        map.SetTile(pos, null);
    }


}
