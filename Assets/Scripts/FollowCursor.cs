using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // distance from camera
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
