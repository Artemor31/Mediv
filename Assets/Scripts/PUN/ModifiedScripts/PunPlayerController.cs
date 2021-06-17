using Characters;
using Photon.Pun;
using UI;
using UnityEngine;

namespace PUN.ModifiedScripts
{
    public class PunPlayerController : MonoBehaviour
    {
        // 3rd person variables
        private const float CameraAngleSpeed = 0.1f;
        private const float CameraPosSpeed = 0.02f;
        private float _cameraPosY = 3f;
        private float _cameraAngleY;

        // UI buttons
        [SerializeField] private FixedButton jumpButton;
        [SerializeField] private FixedButton attackButton;
        [SerializeField] private FixedJoystick leftJoystick;
        [SerializeField] private FixedTouchField touchField;
        [SerializeField] private AnimatorScheduler animator;

        //Rigidbody rigidbody;
        [SerializeField] private Camera _mainCamera;
        private Fighter _fighter;
        private Rigidbody _rigidbody;

        // variables
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float maxSpeed = 5.8f;
    
        // camera positions
        [SerializeField] private float x = 2;
        [SerializeField] private float z = 2;
        [SerializeField] private float rotationOffset = 240;
        
        private bool _onGround;
        private bool _isAttacking;

        //////////PHOTON/////////////////
        private PhotonView _photonView;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _fighter = GetComponent<Fighter>();
            animator = transform.GetChild(0).gameObject.GetComponent<AnimatorScheduler>();
        }

        private void Start()
        {
            //PHOTON setup
            _photonView = GetComponent<PhotonView>();
            if (!_photonView.IsMine)
            {
                var core = transform.parent.gameObject;
                
                var cam = core.GetComponentInChildren<Camera>().gameObject;
                if(cam != null) //Destroy(cam);
                    cam.SetActive(false);
                
                var canvas = core.GetComponentInChildren<Canvas>().gameObject;
                if(canvas != null) //Destroy(canvas);
                    canvas.SetActive(false);
            }
        }

        private void Update()
        {
            if (!_photonView.IsMine) return;
            
            _isAttacking = animator.isAttacking;
            UpdateCharacterState();
            UpdateCameraPosition();
        }


        private void UpdateCharacterState()
        {
            if (_isAttacking) return;
            
            if (attackButton.pressed)
                _fighter.Attack();
            
            Walk();
            Jump();
        }

        private void UpdateCameraPosition()
        {
            // camera position preCalculates
            _cameraAngleY += touchField.TouchDist.x * CameraAngleSpeed;
            _cameraPosY = Mathf.Clamp(_cameraPosY - touchField.TouchDist.y * CameraPosSpeed, 0, 5f);
    
            // Camera transform 
            var position = transform.position;
            Transform mainCameraTransform;
            
            (mainCameraTransform = _mainCamera.transform).position =
                position + (Quaternion.AngleAxis(_cameraAngleY, Vector3.up) * new Vector3(x, _cameraPosY, z));
            _mainCamera.transform.rotation = Quaternion.LookRotation(position + Vector3.up * 2f - mainCameraTransform.position, Vector3.up);
        }

        private void Walk()
        {
            // animator and input
            animator.UpdateMoveSpeed(_rigidbody.velocity.magnitude);
            var input = new Vector3(leftJoystick.input.x, 0, leftJoystick.input.y);

            // move player
            var velocity = Quaternion.AngleAxis(_cameraAngleY + rotationOffset, Vector3.up) * input * maxSpeed;
            _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);

            // Rotate player
            if (Mathf.Abs(input.x) >= 0.01f || Mathf.Abs(input.y) >= 0.01f)
            {
                transform.rotation =
                    Quaternion.AngleAxis(
                        _cameraAngleY + Vector3.SignedAngle( Vector3.forward, input.normalized + Vector3.forward * 0.0001f, Vector3.up) + rotationOffset, 
                        Vector3.up);
            }
        }

        private void Jump()
        {
            _onGround = Physics.Raycast(transform.position + new Vector3(0, 0.05f, 0), Vector3.down, 0.1f);
            animator.OnGround(_onGround);
            if (jumpButton.pressed && _onGround)
            {
                animator.Jump();
                var velocity = _rigidbody.velocity;
                velocity = new Vector3(velocity.x, jumpForce, velocity.z);
                _rigidbody.velocity = velocity;
            }
        }
    }
}