using System.Collections.Generic;
using System.Linq;
using Player.StateMachine;
using UnityEngine;
using Extensions;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        // Inspector
        public ParticleSystem walkParticle;
        public ParticleSystem jumpParticle;
        [Header("Movement")]
        public float movementSpeed;
        [SerializeField] private float directionChangeSpeed;
        [SerializeField] private float bodyRotationSpeed;
        [Range(1,3)] public float sprintMultiplier;
        [Header("Jump")]
        public List<float> jumpForces;
        
        
        // Public
        internal const float GroundedVelocityLimit = -0.1f;
        internal BaseState CurrentState { get; set; }
        internal Rigidbody Rigid { get; private set; }
        internal Animator PlayerAnimator { get; private set; }
        internal int JumpCount { get; set; }
        internal float CurrentSpeed { get; set; }
        
        internal bool IsJumpPressed { get; private set; }
        internal bool IsRunning => _movementInput.magnitude != 0;
        internal bool IsSprinting { get; private set; }
        internal bool OnGround { get; private set; }
        internal bool OnWalkableEnvironment { get; private set; }
        
        // String Hashes
        internal int IsJumpingHash { get; } = Animator.StringToHash("b_isJumping");
        internal int IsRunningHash { get; } = Animator.StringToHash("b_isRunning");
        internal int IsSprintingHash { get; } = Animator.StringToHash("b_isSprinting");
        
        // Privates
        private const double BottomFlagLength = 0.3;

        private StateFactory _states;
        private InputController _input;
        private Transform _camTransform;
        private CapsuleCollider _collider;

        private Vector3 _movementVector = Vector3.zero;
        private Vector2 _movementInput;

        private void Awake()
        {
            StateMachineInit();
            InputInit();
        }
        private void StateMachineInit()
        {
            _states = new StateFactory(this);
            CurrentState = _states.Grounded();
            CurrentState.EnterState();
            CurrentState.InitSubState();
        }
        private void InputInit()
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

        private void Start()
        {
            _camTransform = Camera.main.transform;
            _collider = GetComponent<CapsuleCollider>();
            
            CurrentSpeed = movementSpeed;
            PlayerAnimator = GetComponent<Animator>();
            Rigid = GetComponent<Rigidbody>();
        }
        
        private void FixedUpdate()
        {
            CurrentState.UpdateStates();
            HandleRotation();
            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector3 direction = (_camTransform.forward * _movementInput.y + _camTransform.right * _movementInput.x);
            direction = direction.ResetHeight().normalized;
            
            _movementVector.eSlerp(direction, Time.fixedDeltaTime * directionChangeSpeed);
            
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
                bool collidedFromBottom = 
                    other.contacts
                    .Any(contactPoint => contactPoint.point.y < transform.position.y + BottomFlagLength);

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
        private bool IsGrounded()
        {
            Bounds bounds;
            return Physics.CheckCapsule(
                (bounds = _collider.bounds).center,
                new Vector3(bounds.center.x, bounds.min.y - 0.1f, bounds.center.z), 
                0.18f);
        }
    }
}
