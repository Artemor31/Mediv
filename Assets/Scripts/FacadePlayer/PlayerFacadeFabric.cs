public class PlayerFacadeFabric
{
    // Create PlayerFacadeData to store values and not new them there
    private readonly InputSystem _inputSystem;
    private readonly CharacterAnimator _animator;

    public PlayerFacadeFabric(InputSystem inputSystem, CharacterAnimator animator)
    {
        _inputSystem = inputSystem;
        _animator = animator;
    }

    public PlayerFacade GetMobilePlayer()
    {
        return new PlayerFacade(_inputSystem, new Attacker(), new Mover(), new Caster(), _animator);
    }
}