using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        private const double BottomFlagLength = 0.2;

        [SerializeField]
        private float MovementSpeed;
        [SerializeField]
        private float MovementRotationSpeed;
        [SerializeField]
        private float bodyRotationSpeed;
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

        private bool isRunning => movementInput.magnitude != 0;
        private bool isGround;
        private bool isWalkable;
        
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

            InputHandler();
            AnimationHandler();
        }
        private void InputHandler()
        {
            Debug.Log("InputHandler created.");
            input.Player.WASD.performed += context => movementInput = context.ReadValue<Vector2>();
            input.Player.Shift.started += _ => MovementSpeed += ShiftBoost;
            input.Player.Shift.canceled += _ => MovementSpeed -= ShiftBoost;
            input.Player.Space.started += _ =>
            {
                if (isGround || isWalkable) PlayerJump();
            };
        }
        private void AnimationHandler()
        {
            input.Player.WASD.performed += _ => animator.SetBool(BIsRunning, isRunning); Debug.Log("WASD");
            input.Player.Shift.performed += _ => animator.SetBool(BIsSprinting, true);
            input.Player.Shift.canceled += _ => animator.SetBool(BIsSprinting, false);
            input.Player.Space.performed += _ =>
            { if (isGround || isWalkable) animator.SetBool(BIsJumping, true); };
        }
        private void FixedUpdate()
        {
            PlayerMovement();
            PlayerRotation();
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
                                             bodyRotationSpeed * Time.fixedDeltaTime);
        }
        
        private void PlayerJump() => body.AddForce(Vector3.up * JumpForce * body.mass, ForceMode.Impulse);
        
        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Col enter : " + other.gameObject.tag);

            if (other.gameObject.CompareTag("Ground"))
            {
                isGround = true;
                animator.SetBool(BIsJumping, false);
            }
            
            else if (other.gameObject.CompareTag("Walkable"))
            {
                bool isLandedOn = other.contacts
                    .Any(contactPoint => contactPoint.point.y < transform.position.y + BottomFlagLength);
                if (!isLandedOn) return;
                
                isWalkable = true;
                animator.SetBool(BIsJumping, false);
            }
            
        }
        
        private void OnCollisionExit(Collision other)
        {
            Debug.Log("Col exit : " + other.gameObject.tag);
            if (other.gameObject.CompareTag("Ground"))
                isGround = false;

            else if (other.gameObject.CompareTag("Walkable"))
                isWalkable = false;
        }
    }
}
