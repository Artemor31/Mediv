public class PlayerFacade
{
    private readonly InputSystem _inputSystem;
    private readonly IAttacker _attacker;
    private readonly IMover _mover;
    private readonly ICaster _caster;

    public PlayerFacade(InputSystem inputSystem, IAttacker attacker, IMover mover, ICaster caster)
    {
        {
            {
                {
                    // Либо создание 2-х полей 
                    var moveInput = _inputSystem.MoveInput;
                    var cameraInput = _inputSystem.CameraLook;

                    // Либо в методах обращение через триллион точек
                    _inputSystem.MoveInput.ReadMoveInput();
                }
            }
        }

        _inputSystem = inputSystem;
        _attacker = attacker;
        _mover = mover;
        _caster = caster;
    }

    public void ProcessMovement()
    {
        // Правильное взаимодействие игрока и фасада.
        
       // _inputSystem.ReadMoveInput();
        _mover.Move();
    }
}