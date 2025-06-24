using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerLocomotion : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int IsMovingParam = Animator.StringToHash("IsMoving");

        private const float MovementThreshold = 0.1f;

        private bool _isMoving;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            PlayerController.OnMoveInput += HandleMovementInput;
        }

        private void HandleMovementInput(Vector3 moveDirection)
        {
            bool isMoving = moveDirection.magnitude > MovementThreshold;

            if (isMoving != _isMoving) // only update if changed
            {
                _isMoving = isMoving;

                if (_isMoving)
                {
                    // Play Walk animation
                    _animator.Play("Walk");
                }
                else
                {
                    // Play Idle animation
                    _animator.Play("Idle");
                }

                _animator.SetBool(IsMovingParam, _isMoving); // Optional if you still want the param
            }
        }

        private void OnDestroy()
        {
            PlayerController.OnMoveInput -= HandleMovementInput;
        }
    }
}
