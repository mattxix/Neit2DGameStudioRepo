using System.Collections;
using UnityEngine;

public class GhostSizeChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SetScale(0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SetScale(float formerS, float newS)
    {
        for (float i = 0; i < 10; i++)
        {
            transform.Find("Visual").transform.localScale = Vector3.one * Mathf.Lerp(formerS, newS, i/10);
            yield return new WaitForSeconds(0.03f);
        }
    }


}
