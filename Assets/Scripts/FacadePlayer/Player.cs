using System;
using UnityEditor;
using UnityEngine;
using Weapons;

public class Player : MonoBehaviour
{
    private void Awake()
    {
    }
}

public class PlayerFacadeInstaller
{
    // Create PlayerFacadeData to store values and not new them there
    private readonly IInputSystem _inputSystem = new MobileInput();
    private readonly IAttacker _attacker = new Attacker();
    private readonly IMover _mover = new Mover();
    private readonly ICaster _caster = new Caster();
    private PlayerFacade _playerFacade;

    public PlayerFacade FacadeInit()
    {
        return new PlayerFacade(_inputSystem, _attacker, _mover, _caster);
    }
}

public class PlayerFacade
{
    private readonly IInputSystem _inputSystem;
    private readonly IAttacker _attacker;
    private readonly IMover _mover;
    private readonly ICaster _caster;

    public PlayerFacade(IInputSystem inputSystem, IAttacker attacker, IMover mover, ICaster caster)
    {
        _inputSystem = inputSystem;
        _attacker = attacker;
        _mover = mover;
        _caster = caster;
    }

    public void ProcessMovement()
    {
        // Правильное взаимодействие игрока и фасада.
        
        _mover.Move();
        _inputSystem.ReadMoveInput();
    }
}

public class Caster : ICaster
{
    public void CastSpell(ISpell spell)
    {
        throw new NotImplementedException();
    }
}

public class Mover : IMover
{
    public void Move()
    {
        throw new NotImplementedException();
    }

    public void Jump()
    {
        throw new NotImplementedException();
    }
}

public class Attacker : IAttacker
{
    public void Attack()
    {
        throw new NotImplementedException();
    }
}

public class MobileInput : IInputSystem
{
    public event Action OnJump;
    public event Action OnAttack;
    public event Action OnInteract;
    
    public Vector2 ReadCameraInput()
    {
        throw new NotImplementedException();
    }

    public Vector3 ReadMoveInput()
    {
        throw new NotImplementedException();
    }

}

public interface IInputSystem
{
    public event Action OnJump;
    public event Action OnAttack;
    public event Action OnInteract;
    public Vector2 ReadCameraInput();
    public Vector3 ReadMoveInput();
}

public interface IAttacker
{
    public void Attack();
}

public abstract class WeaponHolder
{
    public abstract IMeleeWeapon MeleeWeapon { get; set; }
    public abstract IRangeWeapon RangeWeapon { get; set; }
}

public interface IRangeWeapon
{
    
}

public interface IMeleeWeapon
{
    
}

public interface ISpell
{
    
}

public interface ICaster
{
    public void CastSpell(ISpell spell);
}

public interface IMover
{
    public void Move();
    public void Jump();
}

