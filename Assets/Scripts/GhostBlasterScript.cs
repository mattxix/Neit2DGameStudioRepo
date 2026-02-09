using System.Collections;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class GhostBlasterScript : MonoBehaviour
{

    private Transform shootFromPoint;
    public int suckDistance;
    public float suckSpeed = 3.0f;
    public LayerMask playerLayer;
    public float usageAmt;
    private bool overloaded = false;
    private bool canSuck = true;
    public Image progressBar;
    public ParticleSystem particleSuck;

    public float powerConsumption = 8f;
    public float powerRecharge = 10f;

    Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        originalColor = progressBar.color;

        shootFromPoint = transform.Find("FirePoint").transform;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!overloaded)
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
                usageAmt += Time.deltaTime * powerConsumption;

                
                
            }
            else
            {
                usageAmt -= Time.deltaTime * powerRecharge;
                
            }
            
        }
        usageAmt = Mathf.Clamp(usageAmt, 0f, 100f);

        if(usageAmt >= 100 && !overloaded)
        {
            overloaded = true;
            StartCoroutine(JammedGun());
        }

        progressBar.fillAmount = usageAmt / 100;

        if (Input.GetMouseButton(0))
        {
            if (overloaded)
                particleSuck.Stop();
            else if (!particleSuck.isPlaying)
                particleSuck.Play();

        }
        else
        {
            if (particleSuck.isPlaying)
                particleSuck.Stop();
        }
    }

   

    IEnumerator JammedGun()
    {
        //StartCoroutine(LerpColor(originalColor, Color.red));
        for (float i = 0; i < 25; i++)
        {
            progressBar.color = Color.Lerp(originalColor, Color.red, i / 25);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2);
        //StartCoroutine(LerpColor(progressBar.color, originalColor));
        for (float i = 0; i < 25; i++)
        {
            progressBar.color = Color.Lerp(progressBar.color, originalColor, i / 25);
            yield return new WaitForSeconds(0.01f);
        }
        overloaded = false;

    }



    IEnumerator LerpColor(Color oldColor, Color newColor)
    {
        for (float i = 0; i < 50; i++)
        {
            progressBar.color = Color.Lerp(oldColor, newColor, i / 50);
            yield return new WaitForSeconds(0.01f);
        }
    }

    
}
