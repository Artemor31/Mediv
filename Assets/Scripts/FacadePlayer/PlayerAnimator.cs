using Characters;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    private static readonly int Died = Animator.StringToHash("Die");
    private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
    private static readonly int Ground = Animator.StringToHash("OnGround");
    private static readonly int GOAttack = Animator.StringToHash("goAttack");

    private PlayerAnimator(){}
    
    public PlayerAnimator(Animator animator)
    {
        _animator = animator;
    }
    
    public override void Die()
    {
        _animator.SetTrigger(Died);
    }

    public override void UpdateMoveSpeed(float speed)
    {
        _animator.SetFloat(MoveSpeed, speed);
    }

    public void OnGround(bool onGround)
    {
        _animator.SetBool(Ground, onGround);
    }

    public override void Attack()
    {
        _animator.SetBool(GOAttack, true);
    }
    public void Jump()
    {
        _animator.Play("Jump");
        _animator.SetBool(Ground, false);
    }

    public void Roll()
    {
        _animator.Play("Roll");
    }
}