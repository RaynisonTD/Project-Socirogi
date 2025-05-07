using UnityEngine;

namespace Player
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
            // Check if there is movement input
            if (_moveDirection.magnitude > 0)
            {
                // Calculate forward/backward magnitude based on movement and look direction
                Vector3 normalizedLookAt = _lookDirection - transform.position;
                normalizedLookAt.Normalize();
                float forwardBackwardsMagnitude = Mathf.Clamp(Vector3.Dot(_moveDirection, normalizedLookAt), -1, 1);

                // Calculate right/left magnitude based on perpendicular vector
                Vector3 perpendicularLookAt = new Vector3(normalizedLookAt.z, 0, -normalizedLookAt.x);
                float rightLeftMagnitude = Mathf.Clamp(Vector3.Dot(_moveDirection, perpendicularLookAt), -1, 1);

                // Set animator parameters for movement
                _animator.SetBool(IsMovingParam, true);
                _animator.SetFloat(ForwardParam, forwardBackwardsMagnitude);
                _animator.SetFloat(RightParam, rightLeftMagnitude);
            }
            else
            {
                // If no movement input, reset the movement parameters to zero
                _animator.SetBool(IsMovingParam, false);
                _animator.SetFloat(ForwardParam, 0f);
                _animator.SetFloat(RightParam, 0f);
            }
        }

        private void OnDestroy()
        {
            PlayerController.OnMoveInput -= HandleMovementInput;
            PlayerController.OnLookInput -= HandleLookInput;
        }
    }
}
