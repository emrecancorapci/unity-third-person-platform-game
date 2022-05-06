using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public float movementSpeed;
        [SerializeField] private float directionChangeSpeed;
        [SerializeField] private float bodyRotationSpeed;
        [Range(1,3)] public float sprintMultiplier;
        public List<float> jumpForces;
        
        // Public
        
        public PlayerBaseState CurrentState { get; set; }
        public Rigidbody Rigid { get; private set; }
        public Animator PlayerAnimator { get; private set; }

        public bool IsJumpPressed { get; internal set; }
        public int JumpCount { get; internal set; }
        internal int IsJumpingHash { get; } = Animator.StringToHash("b_isJumping");
        internal int IsRunningHash { get; } = Animator.StringToHash("b_isRunning");
        internal int IsSprintingHash { get; } = Animator.StringToHash("b_isSprinting");

        public bool IsRunning => _movementInput.magnitude != 0;
        public bool IsSprinting { get; private set; }
        public bool OnGround { get; private set; }
        public bool OnWalkableEnvironment { get; private set; }
        
        // Privates
        
        private const double BottomFlagLength = 0.3;

        private PlayerStateFactory _states;
        private InputController _input;
        private Transform _camTransform;

        private Vector3 _movementVector = Vector3.zero;
        private Vector2 _movementInput;

        public float CurrentSpeed { get; internal set; }

        private void Awake()
        {
            _states = new PlayerStateFactory(this);
            CurrentState = _states.Grounded();
            CurrentState.EnterState();
            CurrentState.InitSubState();

            _camTransform = Camera.main.transform;
            PlayerAnimator = GetComponent<Animator>();
            Rigid = GetComponent<Rigidbody>();
            CurrentSpeed = movementSpeed;
            
            InputHandler();
        }
        private void InputHandler()
        {
            _input = new InputController();
            _input.Enable();
            
            InputController.PlayerActions playerInput = _input.Player;
            playerInput.WASD.performed += context => _movementInput = context.ReadValue<Vector2>();
            playerInput.Shift.started += _ => IsSprinting = true;
            playerInput.Shift.canceled += _ => IsSprinting = false;
            playerInput.Space.started += _ => IsJumpPressed = true;
            playerInput.Space.canceled += _ => IsJumpPressed = false;
        }

        private void Update() => CurrentState.UpdateStates();

        private void FixedUpdate()
        {
            HandleRotation();
            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector3 direction = (_camTransform.forward * _movementInput.y + _camTransform.right * _movementInput.x);
            direction = new Vector3(direction.x, 0, direction.z).normalized;
            
            _movementVector = Vector3.Slerp(_movementVector, direction, Time.fixedDeltaTime * directionChangeSpeed);
            
            transform.position += _movementVector * (CurrentSpeed * Time.fixedDeltaTime);
        }
        private void HandleRotation()
        {
            Vector3 camForward = _camTransform.forward;
            Vector3 camRight = _camTransform.right;
            
            float targetX = camForward.x * _movementInput.y + camRight.x * _movementInput.x;
            float targetZ = camForward.z * _movementInput.y + camRight.z * _movementInput.x;
            
            Vector3 targetDirection = new Vector3(targetX, 0, targetZ).normalized;
            
            transform.forward = Vector3.Slerp(transform.forward, targetDirection,
                bodyRotationSpeed * Time.fixedDeltaTime);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            // Debug.Log("Col enter : " + other.gameObject.tag);

            if (other.gameObject.CompareTag("Ground"))
                OnGround = true;

            else if (other.gameObject.CompareTag("Walkable"))
            {
                double bottomFlagPosition = transform.position.y + BottomFlagLength;
                bool collidedFromBottom = 
                    other.contacts
                    .Any(contactPoint => contactPoint.point.y < bottomFlagPosition);

                if (!collidedFromBottom) return;
                
                OnWalkableEnvironment = true;
            }
        }
        
        private void OnCollisionExit(Collision other)
        {
            // Debug.Log("Col exit : " + other.gameObject.tag);
            
            if (other.gameObject.CompareTag("Ground"))
                OnGround = false;

            else if (other.gameObject.CompareTag("Walkable"))
                OnWalkableEnvironment = false;
        }
    }
}
