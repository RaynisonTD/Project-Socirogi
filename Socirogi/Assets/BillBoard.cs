using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        // Get the camera from Cinemachine
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        // Make sure the object faces the camera from an isometric perspective
        Vector3 direction = transform.position - cam.position;
        direction.y = 0;  // Lock rotation on the Y-axis
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
