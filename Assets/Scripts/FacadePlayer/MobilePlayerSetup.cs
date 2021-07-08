using UI;
using UnityEngine;

public class MobilePlayerSetup : MonoBehaviour
{
    [SerializeField] private PlayerSpawner _spawner;
    [SerializeField] private Animator _animator;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private FixedTouchField _touchField;
    [SerializeField] private Player _player;
    [SerializeField] private FixedButton _button;

    private void OnEnable()
    {
        var input = DependencyInjector.Bind((IMoveInput) null, _joystick);
        var look = DependencyInjector.Bind((ICameraLook) null, _touchField);
        var actions = DependencyInjector.Bind((IPlayerActions) null, _button);
        
        var inputSystem = new MobileInput(input, look, actions);
        var animator = new PlayerAnimator(_animator);
        
        var fabric = new PlayerFacadeFabric(animator);
        var installer = new PlayerServiceInstaller(inputSystem, fabric);
        installer.SetupServices(_player, _spawner);
    }
}