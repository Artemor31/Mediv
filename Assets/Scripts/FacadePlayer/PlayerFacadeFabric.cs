public class PlayerFacadeFabric
{
    // Create PlayerFacadeData to store values and not new them there
    private readonly InputSystem _inputSystem;
    private readonly CharacterAnimator _animator;

    public PlayerFacadeFabric(CharacterAnimator animator)
    {
        _animator = animator;
    }

    public PlayerFacade GetMobilePlayer(IAttacker attacker, IMover mover, ICaster caster)
    {
        return new PlayerFacade(attacker, mover, caster, _animator);
    }
}