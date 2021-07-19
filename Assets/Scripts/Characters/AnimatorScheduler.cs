using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters
{
    public class AnimatorScheduler : MonoBehaviour
    {
        
        public event Action OnAttackEnded;
        
        private static readonly int Ground = Animator.StringToHash("OnGround");
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
        private static readonly int Die1 = Animator.StringToHash("Die");
        
        // variables
        [FormerlySerializedAs("_animator")] 
        [SerializeField] public Animator animator;

        [SerializeField] private PlayerController _player;
        [SerializeField] private AttackBehaviour[] _attackBehaviours;

        private void Start()
        {
            animator = GetComponent<Animator>();
            foreach (var behaviour in _attackBehaviours)
            {
                behaviour.Player = _player;
                behaviour.Animator = animator;
            }
        }

        public void endRoll()
        {
            _player.IsRoll = false;
        }

        public Animator GetAnimatorController()
        {
            return animator;
        }
    
        // on jump button clicked
        public void Jump()
        {
            animator.Play("Jump");
            animator.SetBool(Ground, false);
        }

        public void Roll()
        {
            animator.Play("Roll");
        }
    
        // on attack
        public void Attack()
        {
            //isAttacking = true;
            //animator.SetInteger(AttackType, attackTypeNumber);
           // animator.Play("Attack1");
        }
    
        // attack end event
        public void AttackEnded()
        {
            //isAttacking = false;
           // OnAttackEnded?.Invoke();
        }
    
        // on grounding after jump
        public void OnGround(bool onGround)
        {
            animator.SetBool(Ground, onGround);
        }
    
        // on joystick move
        public void UpdateMoveSpeed(float speed)
        {
            animator.SetFloat(MoveSpeed, speed);
        }
    
        // if hp == 0
        public void Die()
        {
            animator.SetTrigger(Die1);
        }
    
        // attack event
        public void Hit()
        {
        }
    }
}
