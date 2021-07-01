using Characters;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    private static readonly int Died = Animator.StringToHash("Die");
    private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
    private static readonly int Ground = Animator.StringToHash("OnGround");

    private bool _onGround;

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

    public void OnGround()
    {
        _animator.SetBool(Ground, _onGround);
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}