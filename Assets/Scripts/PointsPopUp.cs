using TMPro;
using UnityEngine;

public class PointsPopUp : MonoBehaviour
{
    public TMP_Text text;
    public float floatUpSpeed = 1.5f;
    public float lifeTime = 0.8f;

    private float t;

    public void Setup(int amount)
    {
        if (text != null) text.text = amount.ToString();
    }

    void Update()
    {
        // move up
        transform.position += Vector3.up * floatUpSpeed * Time.deltaTime;

        // fade out
        t += Time.deltaTime;
        float a = Mathf.Clamp01(1f - (t / lifeTime));
        if (text != null)
        {
            Color c = text.color;
            c.a = a;
            text.color = c;
        }

        if (t >= lifeTime)
            Destroy(gameObject);
    }
}
