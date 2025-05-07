using Stats;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector] public float moveSpeed => stats.realTimeStats.movespeed;
        private PlayerStatsComponent stats;
        PlayerInput input;
        private Vector3 _moveDirection;
        private Vector3 _lookDirection;
        private Camera _mainCamera;
        private bool _hasAttacked;
        private Rigidbody _rb;
        private float dodgeForce = 1000f;

       

        // Events for input
        public static event System.Action<Vector3> OnMoveInput;
        public static event System.Action<Vector3> OnLookInput;
        public static event System.Action OnAttackInput;
        public static event System.Action OnInventoryInput;
        public static event System.Action OnDodgeInput;

        void Start()
        {
            stats = GetComponent<PlayerStatsComponent>();
            _mainCamera = Camera.main;
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
            
            
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            // Read the 2D input vector from the input context (e.g., from a joystick or WASD keys).
            Vector2 input = context.ReadValue<Vector2>();

            // If the input has been canceled (e.g., key released), reset movement and notify listeners.
            if (context.canceled) 
            {
                _moveDirection = Vector3.zero;               // Stop movement
                OnMoveInput?.Invoke(_moveDirection);         // Notify any listeners of the change
                return;
            }

            // Get the camera's forward and right vectors to move relative to the camera's orientation.
            Vector3 forward = _mainCamera.transform.forward;
            Vector3 right = _mainCamera.transform.right;

            // Zero out the Y component to ensure movement stays on the horizontal plane.
            forward.y = 0;
            right.y = 0;

            // Calculate the movement direction based on input and camera orientation.
            _moveDirection = (forward * input.y + right * input.x).normalized;

            // Notify any listeners (e.g., a character controller) of the new movement direction.
            OnMoveInput?.Invoke(_moveDirection);
        }

        // Control the Looking direction and rotation of the player
        public void OnLook(InputAction.CallbackContext context)
        {
            // Check if the input is coming from a mouse device
            if (Mouse.current != null && context.control.device == Mouse.current)
            {
                Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
                Ray ray = _mainCamera.ScreenPointToRay(mouseScreenPosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    // Calculate a look point on the same horizontal level as the object
                    _lookDirection = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    Vector3 directionToMouse = _lookDirection - transform.position;
                    directionToMouse.y = 0f; // Ensure there's no vertical offset

                    if (directionToMouse != Vector3.zero)
                        transform.rotation = Quaternion.LookRotation(directionToMouse);

                    // Notify listeners with the computed look direction
                    OnLookInput?.Invoke(_lookDirection);
                }
            }
            // Check if the input is coming from a gamepad device
            else if (context.control.device is Gamepad)
            {
                Vector2 stickInput = context.ReadValue<Vector2>();
                Vector3 lookDirection = new Vector3(stickInput.x, 0f, stickInput.y);

                if (lookDirection.sqrMagnitude > 0.01f) // Only proceed if input is significant
                {
                    transform.rotation = Quaternion.LookRotation(lookDirection.normalized);
                    _lookDirection = transform.position + lookDirection;

                    // Notify listeners with the computed look direction
                    OnLookInput?.Invoke(_lookDirection);
                }
            }
        }


        public void OnAttack(InputAction.CallbackContext context)
        {
            

            if (context.performed)
                OnAttackInput?.Invoke();
        }

        public void OnInventory(InputAction.CallbackContext context)
        {
            

            if (context.performed)
            {
                OnInventoryInput?.Invoke();
                Debug.Log("pressed I");
            }
        }

        public void OnDodge(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                print("dodge pressed");
                return;
                
                
            }

            StartCoroutine(DodgeRoutine());
        }

        private IEnumerator DodgeRoutine()
        {
           

            // Settings
            float dodgeDistance = 5f;
            float dodgeDuration = 0.3f;
            float elapsed = 0f;

            Vector3 start = transform.position;

            // If no input, dodge forward in facing direction
            Vector3 direction = _moveDirection.sqrMagnitude > 0.01f
                ? _moveDirection.normalized
                : transform.forward;

            Vector3 end = start + direction * dodgeDistance;

            // Optional: Trigger dodge animation
            // animator.SetTrigger("Dodge");

            while (elapsed < dodgeDuration)
            {
                transform.position = Vector3.Lerp(start, end, elapsed / dodgeDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = end;

            
        }


        void FixedUpdate()
        {
            

            Vector3 velocity = _moveDirection * moveSpeed;
            _rb.linearVelocity = velocity;
        }
    }
}
