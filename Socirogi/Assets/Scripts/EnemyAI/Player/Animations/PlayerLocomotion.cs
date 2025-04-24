using UnityEngine;

namespace Player.Animations
{
    [RequireComponent(typeof(Animator))]
    public class PlayerLocomotion : MonoBehaviour
    {
        private Animator _animator;
        private Vector3 _moveDirection;
        private Vector3 _lookDirection;

        private static readonly int ForwardParam = Animator.StringToHash("Forward");
        private static readonly int RightParam = Animator.StringToHash("Right");
        private static readonly int IsMovingParam = Animator.StringToHash("IsMoving");
        
        private float _currentForward;
        private float _currentRight;
        [SerializeField] private float _smoothTime = 0.15f;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            PlayerController.OnMoveInput += HandleMovementInput;
            PlayerController.OnLookInput += HandleLookInput;
        }

        private void HandleMovementInput(Vector3 moveDirection)
        {
            _moveDirection = moveDirection;
            UpdateAnimator();
        }

        private void HandleLookInput(Vector3 lookDirection)
        {
            _lookDirection = lookDirection;
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            if (_moveDirection.magnitude > 0.1f)
            {
                Vector3 normalizedLookAt = (_lookDirection - transform.position).normalized;

                float forwardBackwardsMagnitude = Vector3.Dot(_moveDirection.normalized, normalizedLookAt);
                Vector3 perpendicularLookAt = new Vector3(normalizedLookAt.z, 0, -normalizedLookAt.x);
                float rightLeftMagnitude = Vector3.Dot(_moveDirection.normalized, perpendicularLookAt);

                // Smooth afbouwen of opbouwen naar target
                _currentForward = Mathf.Lerp(_currentForward, forwardBackwardsMagnitude, Time.deltaTime / _smoothTime);
                _currentRight = Mathf.Lerp(_currentRight, rightLeftMagnitude, Time.deltaTime / _smoothTime);

                _animator.SetBool(IsMovingParam, true);
                _animator.SetFloat(ForwardParam, _currentForward);
                _animator.SetFloat(RightParam, _currentRight);
            }
            else
            {
                _animator.SetBool(IsMovingParam, false);

                // Soepel terug naar nul als we stoppen met bewegen
                _currentForward = Mathf.Lerp(_currentForward, 0f, Time.deltaTime / _smoothTime);
                _currentRight = Mathf.Lerp(_currentRight, 0f, Time.deltaTime / _smoothTime);

                _animator.SetFloat(ForwardParam, _currentForward);
                _animator.SetFloat(RightParam, _currentRight);
            }
        }


        private void OnDestroy()
        {
            PlayerController.OnMoveInput -= HandleMovementInput;
            PlayerController.OnLookInput -= HandleLookInput;
        }
    }
}
