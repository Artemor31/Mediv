using System.Collections;
using UnityEngine;
using UI;

namespace Characters
{
    public class PlayerController : MonoBehaviour, IState
    {
        public CharacterState State { get; private set; }

        // UI buttons
        [SerializeField] private FixedButton jumpButton;
        [SerializeField] private FixedButton attackButton;
        [SerializeField] private FixedButton actionButton;
        [SerializeField] private AnimatorScheduler _animator;
        
        // private variables
        private Fighter _fighter;
        private Rigidbody _rigidbody;

        // variables
        [SerializeField] private float _jumpForce = 5f;

        // player state checkers
        private bool _onGround;
        public bool GoAttack;
        public bool IsRoll;
        public float RollSpeed;

        private StateResolve _resolver;
        private Mover _mover;
        private InputSystem _inputSystem;
        private PlayerCamera _playerCamera;
        private Jumper _jumper;


        private void Awake()
        {
            _resolver = new StateResolve(this);
            _playerCamera = new PlayerCamera(_inputSystem, Camera.main);
            _mover = new Mover(_inputSystem, _resolver, _playerCamera);
            _rigidbody = GetComponent<Rigidbody>();
            _fighter = GetComponent<Fighter>();
            _jumper = new Jumper(_rigidbody, transform);
            _animator = transform.GetChild(0).GetComponent<AnimatorScheduler>();

            attackButton.OnAttacked += Attack;
            jumpButton.OnJumped += _jumper.Jump;
            actionButton.OnRolled += Roll;
        }
        
        private void FixedUpdate()
        {
            CheckOnRoll();
        }

        private void Update()
        {
        }

        private void CheckOnRoll()
        {
            if (IsRoll)
            {
                _rigidbody.velocity = transform.forward * (RollSpeed * Time.deltaTime);
            }
        }

        private void Roll()
        {
            if (_resolver.CanRoll == false) return;
            _animator.Roll();
            IsRoll = true;
        }

        private void Attack()
        {
            if (_resolver.CanAttack == false) return;
            GoAttack = true;
            _animator.animator.SetBool("goAttack", true);
            _fighter.Attack();
        }

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