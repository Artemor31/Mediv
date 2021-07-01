using UnityEngine;

public class MobilePlayerInstaller : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private MobileInput _moveInput;
    [SerializeField] private MobileCamera _camera;

    private void OnEnable()
    {
        //var inputSystem = new MobileInput()
        //_player.Init();
    }
}