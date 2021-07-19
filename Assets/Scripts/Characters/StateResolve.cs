using Characters;

public class StateResolve
{
    private readonly IState _character;
    
    public bool CanRoll => _character.State != CharacterState.Jump;
    public bool CanAttack => _character.State == CharacterState.Idle;
    public bool CanCast => _character.State == CharacterState.Idle;
    public bool CanHurt => _character.State == CharacterState.Idle;
    public bool CanJump => _character.State == CharacterState.Idle;
    public bool CanWalk => _character.State == CharacterState.Idle;
    
    public StateResolve(IState character)
    {
        _character = character;
    }
}