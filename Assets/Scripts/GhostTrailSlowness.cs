using UnityEngine;

public class GhostTrailSlowness : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float slowSpeed = 2f;

    private float currentSpeed;

    void Start()
    {
        currentSpeed = normalSpeed;
    }

    void Update()
    {
   
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(h, v) * currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GhostTrail"))
        {
            currentSpeed = slowSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GhostTrail"))
        {
            currentSpeed = normalSpeed;
        }
    }
}

