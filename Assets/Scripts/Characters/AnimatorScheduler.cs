using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters
{
    public class AnimatorScheduler : MonoBehaviour
    {
        public event Action OnAttackEnded;
        
        private static readonly int Ground = Animator.StringToHash("OnGround");
        private static readonly int Attack1 = Animator.StringToHash("Attack");
        private static readonly int AttackType = Animator.StringToHash("AttackType");
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
        private static readonly int Die1 = Animator.StringToHash("Die");
        
        // variables
        [FormerlySerializedAs("_animator")] 
        [SerializeField] private Animator animator;
        public bool isAttacking;


        private void Awake()
        {
            animator = GetComponent<Animator>();
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
    
        // on attack
        public void Attack(int attackTypeNumber)
        {
            isAttacking = true;
            animator.SetInteger(AttackType, attackTypeNumber);
            animator.SetTrigger(Attack1);
        }
    
        // attack end event
        public void AttackEnded()
        {
            isAttacking = false;
            OnAttackEnded?.Invoke();
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
