using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class WoodPileScript : MonoBehaviour
{
    GameObject[] windows;
    public bool isHoldingWood;
    public Image pileImage; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isHoldingWood = !isHoldingWood;
           // Debug.Log("Hello wood");
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (isHoldingWood)
        {
            pileImage.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            pileImage.color = new Color(0f, 0f, 0f, 0.5f);
        }
    }
}
