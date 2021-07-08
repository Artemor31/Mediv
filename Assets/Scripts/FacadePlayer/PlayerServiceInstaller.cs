public class PlayerServiceInstaller
{
    private readonly PlayerFacadeFabric _playerFacadeFabric;
    private readonly MobileInput _inputSystem;

    public PlayerServiceInstaller(MobileInput inputSystem, PlayerFacadeFabric fabric)
    {
        _inputSystem = inputSystem;
        _playerFacadeFabric = fabric;
    }
    
    public void SetupServices(Player player, PlayerSpawner spawner)
    {
        var attacker = new Attacker();
        var mover = new Mover();
        var caster = new Caster();
        var facade = _playerFacadeFabric.GetMobilePlayer(attacker, mover, caster);
        
        var service = new PlayerService(_inputSystem, facade, spawner);
        
        service.InitPlayer(player);
    }
}