using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        private const double BottomFlagLength = 0.3;

        [SerializeField]
        private float movementSpeed;
        [SerializeField]
        private float directionChangeSpeed;
        [SerializeField]
        private float bodyRotationSpeed;
        [SerializeField]
        private List<float> jumpForces;
        [SerializeField] [Range(1,3)]
        private float sprintMultiplier;
        
        private InputController _input;
        private Rigidbody _rigidbody;
        private Animator _animator;
        private Transform _camTransform;

        private Vector3 _movementVector = Vector3.zero;
        private Vector2 _movementInput;

        private float _currentSpeed;
        private int _jumpCount = 0;
        
        private bool IsRunning => _movementInput.magnitude != 0;
        private bool _onGround;
        private bool _onWalkableEnvironment;
        
        private static readonly int BIsRunning = Animator.StringToHash("b_isRunning");
        private static readonly int BIsJumping = Animator.StringToHash("b_isJumping");
        private static readonly int BIsSprinting = Animator.StringToHash("b_isSprinting");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            _camTransform = Camera.main.transform;

            _currentSpeed = movementSpeed;
            
            _input = new InputController();
            _input.Enable();

            InputHandler();
            AnimationHandler();
        }
        private void InputHandler()
        {
            _input.Player.WASD.performed += context => _movementInput = context.ReadValue<Vector2>();
            _input.Player.Shift.started += _ => _currentSpeed = movementSpeed * sprintMultiplier;
            _input.Player.Shift.canceled += _ => _currentSpeed = movementSpeed;
            _input.Player.Space.started += _ => OnJump();
        }
        private void AnimationHandler()
        {
            _input.Player.WASD.performed += _ => _animator.SetBool(BIsRunning, IsRunning);
            _input.Player.Shift.performed += _ => _animator.SetBool(BIsSprinting, true);
            _input.Player.Shift.canceled += _ => _animator.SetBool(BIsSprinting, false);
            _input.Player.Space.performed += _ => 
                { if (_onGround || _onWalkableEnvironment) _animator.SetBool(BIsJumping, true); };
        }
        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }
        private void HandleMovement()
        {
            Vector3 direction = (_camTransform.forward * _movementInput.y + _camTransform.right * _movementInput.x);
            direction = new Vector3(direction.x, 0, direction.z).normalized;
            
            _movementVector = Vector3.Slerp(_movementVector, direction, Time.fixedDeltaTime * directionChangeSpeed);
            
            transform.position += _movementVector * (_currentSpeed * Time.fixedDeltaTime);
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
        
        private void OnJump()
        {
            if (_onGround || _onWalkableEnvironment)
            {
                int jumpType = _jumpCount % jumpForces.Count;
                _rigidbody.AddForce(Vector3.up * jumpForces[jumpType] * _rigidbody.mass, ForceMode.Impulse);
                _jumpCount++;
            }
                
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Col enter : " + other.gameObject.tag);

            if (other.gameObject.CompareTag("Ground"))
            {
                _onGround = true;
                _animator.SetBool(BIsJumping, false);
            }
            
            else if (other.gameObject.CompareTag("Walkable"))
            {
                bool isLandedOn = other.contacts
                    .Any(contactPoint => contactPoint.point.y < transform.position.y + BottomFlagLength);
                if (!isLandedOn) return;
                
                _onWalkableEnvironment = true;
                _animator.SetBool(BIsJumping, false);
            }
            
        }
        
        private void OnCollisionExit(Collision other)
        {
            Debug.Log("Col exit : " + other.gameObject.tag);
            if (other.gameObject.CompareTag("Ground"))
                _onGround = false;

            else if (other.gameObject.CompareTag("Walkable"))
                _onWalkableEnvironment = false;
        }
    }
}
