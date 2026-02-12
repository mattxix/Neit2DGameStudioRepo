using UnityEngine;
using UnityEngine.UI;

public class CursorTrackingScript : MonoBehaviour
{
    private Camera cam;
    public float zPosition = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse position in screen space
        Vector3 mousePositionScreen = Input.mousePosition;

        // Set the Z value to the desired world Z position 
        // (Camera.main.nearClipPlane is a common value for perspective cameras)
        mousePositionScreen.z = zPosition - cam.transform.position.z;

        // Convert screen position to world position
        Vector3 mousePositionWorld = cam.ScreenToWorldPoint(mousePositionScreen);

        // Move the image to the world position
        transform.position = mousePositionWorld;
    }
}
