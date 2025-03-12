using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        private Vector3 _moveDirection;
        private Camera _mainCamera;
        private Vector3 _targetPosition;  // Position where the mouse raycast hits

        void Start()
        {
            // Get reference to the main camera
            _mainCamera = Camera.main;
        }

        // Directly update the movement direction in the OnMove callback.
        public void OnMove(InputAction.CallbackContext context)
        {
            // Get the movement input as Vector2 and convert it to a Vector3.
            _moveDirection = new Vector3(context.ReadValue<Vector2>().x, 0f, context.ReadValue<Vector2>().y).normalized;
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            // Get the mouse position in screen space
            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

            // Create a ray from the camera to the mouse position in screen space
            Ray ray = _mainCamera.ScreenPointToRay(mouseScreenPosition);

            // We want to hit something on the ground, so we cast a ray along the Z and X axes
            // Cast the ray into the scene and get the point of intersection (where it hits the ground)
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // The ray hit something, so we calculate the point on the X and Z axes where the mouse is pointing
                _targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                // Get the direction to the mouse hit point
                Vector3 directionToMouse = _targetPosition - transform.position;

                // Set the player's rotation to face the target point, ignoring the Y axis
                directionToMouse.y = 0f; // Don't rotate along the Y-axis
                transform.rotation = Quaternion.LookRotation(directionToMouse);
            }
        }

        void Update()
        {
            // Apply movement every frame, adding the movement to the current position.
            transform.position += _moveDirection * moveSpeed * Time.deltaTime;
        }

        // This will draw a line from the camera to the world position where the mouse ray hits
        private void OnDrawGizmos()
        {
            if (_targetPosition != Vector3.zero)
            {
                Gizmos.color = Color.red;  // Set the color of the gizmo
                Gizmos.DrawSphere(_targetPosition, 0.1f);  // Draw a small sphere at the target position

                // Draw a line from the camera to the target position
                Debug.DrawLine(_mainCamera.transform.position, _targetPosition, Color.green);
            }
        }
    }
}
