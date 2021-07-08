public class PlayerService
{
    private readonly MobileInput _inputSystem;
    private readonly PlayerFacade _facade;
    private readonly PlayerSpawner _spawner;

    public PlayerService(MobileInput inputSystem, PlayerFacade facade, PlayerSpawner spawner)
    {
        _inputSystem = inputSystem;
        _facade = facade;
        _spawner = spawner;
    }

    public void InitPlayer(Player player)
    {
        player.Init(_inputSystem, _facade);
        _spawner.Spawn(player.gameObject);
    }
}