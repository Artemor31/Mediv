using Characters;

public class PlayerFacade : CharacterFacade
{
    // Add animator component.
    
    protected override Stats Stats { get; set; }
    
    private readonly InputSystem _inputSystem;
    private readonly IAttacker _attacker;
    private readonly IMover _mover;
    private readonly ICaster _caster;
    private readonly CharacterAnimator _animator;

    public PlayerFacade(InputSystem inputSystem, IAttacker attacker, IMover mover, ICaster caster,
        CharacterAnimator animator)
    {
        _inputSystem = inputSystem;
        _attacker = attacker;
        _mover = mover;
        _caster = caster;
        _animator = animator;
    }

    public override void PerformMovement()
    {
        var moveDirection = _inputSystem.MoveInput.ReadMoveInput();
        _mover.Move();
        //_animator.UpdateMoveSpeed();
    }

    public override void PerformAttack()
    {
        _animator.Attack();
        _attacker.Attack(); 
    }
}