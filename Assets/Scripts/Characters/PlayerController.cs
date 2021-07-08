using System.Collections;
using UnityEngine;
using UI;

namespace Characters
{
    public class PlayerController : MonoBehaviour
    {
        // 3rd person variables
        private const float CameraAngleSpeed = 0.1f;
        private const float CameraPosSpeed = 0.02f;
        private float _cameraPosY = 3f;
        private float _cameraAngleY;

        // UI buttons
        [SerializeField] private FixedButton jumpButton;
        [SerializeField] private FixedButton attackButton;
        [SerializeField] private FixedButton actionButton;
        [SerializeField] private FixedJoystick leftJoystick;
        [SerializeField] private FixedTouchField _touchField;
        [SerializeField] private AnimatorScheduler _animator;
        
        // private variables
        private Camera _mainCamera;
        private Fighter _fighter;
        private Rigidbody _rigidbody;

        // variables
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _maxSpeed = 5.8f;
    
        // camera positions
        [SerializeField] private float x = 2;
        [SerializeField] private float z = 2;
        
        // player state checkers
        private bool _onGround;
        public bool GoAttack;
        public bool IsRoll;
        public float RollSpeed;

        
        private void Awake()
        {
            _mainCamera = Camera.main;
            _rigidbody = GetComponent<Rigidbody>();
            _fighter = GetComponent<Fighter>();
            _animator = transform.GetChild(0).GetComponent<AnimatorScheduler>();
            
            
            attackButton.OnAttacked += Attack;
            jumpButton.OnJumped += Jump;
            actionButton.OnRolled += Roll;
        }
        
        private void FixedUpdate()
        {
            UpdateCharacterState();
        }

        private void Update()
        {
            UpdateCameraPosition();
        }

        private void UpdateCharacterState()
        {
            if (IsRoll)
            {
                _rigidbody.velocity = transform.forward * RollSpeed * Time.deltaTime;
            }
            
            OnGroundCheck();
            Walk();
        }

        public void Roll()
        {
            if (!_onGround) return;
            //var rotation = _mainCamera.transform.eulerAngles;
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation.y, transform.eulerAngles.z);
            _animator.Roll();
            IsRoll = true;
        }

        private void UpdateCameraPosition()
        {
            // camera position preCalculates
            _cameraAngleY += _touchField.TouchDist.x * CameraAngleSpeed;
            _cameraPosY = Mathf.Clamp(_cameraPosY - _touchField.TouchDist.y * CameraPosSpeed, 0, 5f);
    
            // Camera transform 
            var position = transform.position;
            Transform mainCameraTransform;
            
            (mainCameraTransform = _mainCamera.transform).position =
                position + Quaternion.AngleAxis(_cameraAngleY - 220f, Vector3.up) 
                * new Vector3(x, _cameraPosY, z);
            
            _mainCamera.transform.rotation = Quaternion.LookRotation(
                position + Vector3.up * 2f - mainCameraTransform.position, 
                Vector3.up);
        }

        private void Attack()
        {
            if (!_onGround) return;
            GoAttack = true;
            _animator.animator.SetBool("goAttack", true);
            _fighter.Attack();
        }
        
        private void Walk()
        {
            if(IsRoll) return;
            if (GoAttack || !_onGround) return;
            
            // animator and input
            _animator.UpdateMoveSpeed(_rigidbody.velocity.magnitude);  //maybe velocity.magnitude/2  ????????????????
            var input = new Vector3(leftJoystick.input.x, 0, leftJoystick.input.y);

            // move player
            var velocity = Quaternion.AngleAxis(_cameraAngleY, Vector3.up) * input * _maxSpeed;
            _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);

            // Rotate player
            if (!(Mathf.Abs(input.x) >= 0.01f) && !(Mathf.Abs(input.y) >= 0.01f)) return;
            var forward = input.normalized + Vector3.forward * 0.0001f;
            var signedAngle = Vector3.SignedAngle(Vector3.forward, forward, Vector3.up);
            var angle = _cameraAngleY + signedAngle;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }

        private void Jump()
        {
            if (!_onGround) return;
            _animator.Jump();
            var velocity = _rigidbody.velocity;
            velocity = new Vector3(velocity.x, _jumpForce, velocity.z);
            _rigidbody.velocity = velocity;
        }

        /// <summary>
        /// Is player on ground.
        /// </summary>
        private void OnGroundCheck()
        {
            _onGround = Physics.Raycast(
                transform.position + new Vector3(0, 0.05f, 0), 
                Vector3.down, 
                0.1f);
            
            _animator.OnGround(_onGround);
        }

        /// <summary>
        /// Interact with objects in game.
        /// </summary>
        private void Interact()
        {
            var targets = Physics.OverlapSphere(transform.position, 2);
            foreach (var target in targets)
            {
                if (target.GetComponent<IInteractible>() == null) continue;
                target.GetComponent<IInteractible>().Interact();
                target.GetComponent<Transform>().LookAt(transform);
                return;
            }
        }
    }
}