using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StormManager : MonoBehaviour
{

    public Light2D globalLight;
    private Color startLightColor;
    public Color endLightColor;
    public float lightningDuration = .05f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startLightColor = globalLight.color;
        StartCoroutine(Storm());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Storm()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 20));
            globalLight.color = endLightColor;
            yield return new WaitForSeconds(lightningDuration);
            globalLight.color = startLightColor;
        }
    }
}
