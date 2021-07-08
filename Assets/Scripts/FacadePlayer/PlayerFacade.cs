using Characters;
using UnityEngine;

public class PlayerFacade : CharacterFacade
{
    private const float X = 2;
    private const float Z = 2;
    
    protected override Stats Stats { get; set; }
    
    private readonly IAttacker _attacker;
    private readonly IMover _mover;
    private readonly ICaster _caster;
    private readonly CharacterAnimator _animator;

    private float _cameraPosY = 3;
    private float _cameraAngleY;

    public PlayerFacade(IAttacker attacker, IMover mover, ICaster caster, CharacterAnimator animator)
    {
        _attacker = attacker;
        _mover = mover;
        _caster = caster;
        _animator = animator;
    }

    public override void PerformMovement(float speed)
    {
        _mover.Move();
        _animator.UpdateMoveSpeed(speed);
    }

    public override void PerformAttack()
    {
        _animator.Attack();
        _attacker.Attack(); 
    }

    public override void PerformCast()
    {
        throw new System.NotImplementedException();
    }

    public override void PerformJump()
    {
        throw new System.NotImplementedException();
    }

    public override void PerformRoll()
    {
        throw new System.NotImplementedException();
    }

    public override void PerformInteraction()
    {
        throw new System.NotImplementedException();
    }

    public override void PerformCameraRotation(Camera camera, Vector2 input, Transform transform)
    {
        // camera position preCalculates
        _cameraAngleY += input.x * 0.1f;
        _cameraPosY = Mathf.Clamp(_cameraPosY - input.y * 0.02f, 0, 5f);

        // Camera transform 
        var position = transform.position;
        Transform mainCameraTransform;

        (mainCameraTransform = camera.transform).position =
            position + Quaternion.AngleAxis(_cameraAngleY - 220f, Vector3.up)
            * new Vector3(X, _cameraPosY, Z);

        camera.transform.rotation = Quaternion.LookRotation(
            position + Vector3.up * 2f - mainCameraTransform.position,
            Vector3.up);
    }
}