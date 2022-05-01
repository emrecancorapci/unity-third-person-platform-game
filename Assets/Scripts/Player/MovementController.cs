using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField]
        private float MovementSpeed;
        [SerializeField]
        private float MovementRotationSpeed;
        [SerializeField]
        private float RotationSpeed;
        [SerializeField]
        private float JumpForce;
        [SerializeField]
        private float ShiftBoost;
        
        
        private InputController input;
        private Rigidbody body;
        private Animator animator;
        private Transform camTransform;

        private Vector3 movementVector = Vector3.zero;
        private Vector2 movementInput;
        private bool _isGround;

        private bool isRunning => movementInput.magnitude != 0;

        private bool IsGround
        {
            get => _isGround;
            set
            {
                Debug.Log("IsGround is " + _isGround); 
                _isGround = value;
            }
        }

        private static readonly int BIsRunning = Animator.StringToHash("b_isRunning");
        private static readonly int BIsJumping = Animator.StringToHash("b_isJumping");
        private static readonly int BIsSprinting = Animator.StringToHash("b_isSprinting");

        private void Awake()
        {
            animator = GetComponent<Animator>();
            body = GetComponent<Rigidbody>();
            camTransform = Camera.main.transform;
            
            input = new InputController();
            input.Enable();
            
            input.Player.WASD.performed += context => movementInput = context.ReadValue<Vector2>();
            input.Player.Space.performed += _ => Jump();
            input.Player.Shift.started += _ =>
            {
                animator.SetBool(BIsSprinting, true);
                MovementSpeed += ShiftBoost;
            };
            input.Player.Shift.canceled += _ =>
            {
                animator.SetBool(BIsSprinting, false);
                MovementSpeed -= ShiftBoost;
            };
        }
        
        private void Update()
        {
            AnimationHandler();
        }

        void FixedUpdate()
        {
            PlayerRotation();
            PlayerMovement();
        }

        private void AnimationHandler()
        {
            animator.SetBool(BIsRunning, isRunning);
        }

        private void PlayerMovement()
        {
            Vector3 targetVector = (camTransform.forward * movementInput.y
                                    + camTransform.right * movementInput.x).normalized;
            targetVector = new Vector3(targetVector.x, 0, targetVector.z);
            movementVector = Vector3.Slerp(movementVector, targetVector, 
                                           Time.fixedDeltaTime * MovementRotationSpeed);
            transform.position += movementVector * (MovementSpeed * Time.fixedDeltaTime);
        }

        private void PlayerRotation()
        {
            float targetX = camTransform.forward.x * movementInput.y + camTransform.right.x * movementInput.x;
            float targetZ = camTransform.forward.z * movementInput.y + camTransform.right.z * movementInput.x;
            Vector3 targetDirection = new Vector3(targetX, 0, targetZ).normalized;
            
            transform.forward = Vector3.Slerp(transform.forward, targetDirection,
                                             RotationSpeed * Time.fixedDeltaTime);
        }

        private void Jump()
        {
            if (!IsGround) return;
            
            animator.SetBool(BIsJumping, true);
            body.AddForce(Vector3.up * JumpForce * body.mass, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Col with : " + other.gameObject.tag);
            
            CheckGround(other);
        }
        
        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                IsGround = false;
            }
        }

        private void CheckGround(Collision colliding)
        {
            if (!colliding.gameObject.CompareTag("Ground")) return;
            
            if (colliding.contacts.Any(contactPoint => contactPoint.point.y < transform.position.y))
            {
                IsGround = true;
                animator.SetBool(BIsJumping, false);
            }
        }
    }
}
