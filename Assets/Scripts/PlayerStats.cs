using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int Cash;
    public TMP_Text cashText;
    void Start()
    {
        Cash = 0;
        UpdateCashText();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cash"))
        {
            Cash += 1;
            UpdateCashText();
            Destroy(other.gameObject);
        }
    }

    private void UpdateCashText()
    {
        if (cashText != null)
        {
            cashText.text = "Cash: " + Cash.ToString();
        }
    }
}
