using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLookahead : MonoBehaviour
{
    public GameObject mouseFollower;          // The invisible object that will follow the mouse
    public Camera mainCamera;                 // Reference to the main camera
    public float raycastDistance = 100f;      // Distance of the raycast to detect the mouse position

    // This method should be called by the OnLook event
    public void OnLook(InputAction.CallbackContext context)
    {
        // Perform the raycast to get where the mouse is pointing in the world
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            // Move the invisible object to the hit position
            mouseFollower.transform.position = hit.point;
        }
    }
}