using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public Animator animator;
    public Transform player;

    [SerializeField]
    public int requiredKills = 3;
    public bool shieldActive = false;


    private int hits;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //AppearShield();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }

    public void AppearShield()
    {
        requiredKills = Random.Range(5, 12);
        Debug.Log("REQUIREDKILLS: "+requiredKills);
        animator.SetTrigger("Appear");
        gameObject.GetComponent<Collider2D>().enabled = true;
        hits = 2;
        shieldActive = true;
    }

    public void PopShield()
    {
        animator.SetTrigger("Pop"); 
        gameObject.GetComponent<Collider2D>().enabled = false;
        shieldActive = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Debug.Log(collision.gameObject.CompareTag("Ghost"));
            if (collision.gameObject.CompareTag("Ghost"))
            {
                if (!collision.gameObject.GetComponent<GhostHealth>().alive)
                {
                    return;
                }
                hits--;
                GameObject.Destroy(collision.gameObject);
                if (hits == 0)
                {
                    PopShield();
                }
            }
        }

    }

}
