using Unity.VisualScripting;
using UnityEngine;

public class RepairWindowScript : MonoBehaviour
{
    public bool windowFixed = false;
    public Transform player;
    public WoodPileScript woodPileScript;
    public GameObject woodBoards;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && woodPileScript.isHoldingWood && !windowFixed)
        {
            windowFixed = true;
            woodPileScript.isHoldingWood = false;
        }
    }

    private void Update()
    {
        if (woodBoards != null)
        {
            woodBoards.SetActive(windowFixed);
        }
    }
}   
