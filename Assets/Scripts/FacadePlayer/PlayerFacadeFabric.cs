

public class PlayerFacadeFabric
{

    // Create PlayerFacadeData to store values and not new them there
    private readonly IAttacker _attacker = new Attacker();
    private readonly IMover _mover = new Mover();
    private readonly ICaster _caster = new Caster();
    
    private readonly InputSystem _inputSystem;

    public PlayerFacadeFabric(InputSystem inputSystem)
    {
        _inputSystem = inputSystem;
    }

    public PlayerFacade GetMobilePlayer()
    {
        return new PlayerFacade(_inputSystem, _attacker, _mover, _caster);
    }
}