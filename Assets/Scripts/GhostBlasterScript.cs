using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class GhostBlasterScript : MonoBehaviour
{

    private Transform shootFromPoint;
    public int suckDistance;
    public float suckSpeed = 3.0f;
    public LayerMask playerLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootFromPoint = transform.Find("FirePoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(shootFromPoint.position, shootFromPoint.up, suckDistance, suckDistance, ~playerLayer);
           // Debug.Log("RAYCASTING");
            if (hitInfo.collider != null)
            {
             // Debug.Log("i hit: "+ hitInfo.collider.name);

                if (hitInfo.collider.gameObject.CompareTag("Ghost"))
                {
                   // Debug.Log("PASSED B");

                    var ghost = hitInfo.collider.gameObject;

                    ghost.GetComponent<GhostHealth>().Kill();
                    //float step = suckSpeed * Time.deltaTime;

                    // Move the object towards the target position
                    /*ghost.transform.position = Vector2.MoveTowards(transform.position, shootFromPoint.position, step);
                    if (Vector2.Distance(ghost.transform.position, shootFromPoint.position) < 5)
                    {

                    }*/
                }
            }

            Debug.DrawLine(transform.position, shootFromPoint.up * suckDistance, Color.red);

        }
    }
}
