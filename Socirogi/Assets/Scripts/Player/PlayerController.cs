using Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector] public float moveSpeed => stats.realTimeStats.movespeed;
        private PlayerStatsComponent stats;
        private Vector3 _moveDirection;
        private Vector3 _lookDirection;
        private Camera _mainCamera;
        private Rigidbody _rb;

        // Event for movement and look input to be sent to the PlayerLocomotion script
        public static event System.Action<Vector3> OnMoveInput;
        public static event System.Action<Vector3> OnLookInput;

        void Start()
        {
            stats = GetComponent<PlayerStatsComponent>();
            _mainCamera = Camera.main;
            _rb = GetComponent<Rigidbody>();  // Haal de Rigidbody van de speler op
            _rb.freezeRotation = true; // Zet rotatiebevriezing aan, zodat de Rigidbody niet automatisch roteert
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            Vector3 forward = _mainCamera.transform.forward;
            Vector3 right = _mainCamera.transform.right;

            forward.y = 0;
            right.y = 0;

            // Bereken de bewegingsrichting op basis van input (vooruit/achteruit en rechts/links)
            _moveDirection = (forward * input.y + right * input.x).normalized;

            // Trigger event voor bewegingsinput
            OnMoveInput?.Invoke(_moveDirection);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
            Ray ray = _mainCamera.ScreenPointToRay(mouseScreenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _lookDirection = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                Vector3 directionToMouse = _lookDirection - transform.position;
                directionToMouse.y = 0f;

                // Rotate speler naar de muispositie
                transform.rotation = Quaternion.LookRotation(directionToMouse);

                // Trigger event voor kijkinput
                OnLookInput?.Invoke(_lookDirection);
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                // Handle aanval (indien van toepassing)
            }
        }

        void FixedUpdate()
        {
            // Beweeg de speler met behulp van de Rigidbody
            Vector3 velocity = _moveDirection * moveSpeed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + velocity);
        }
    }
}
