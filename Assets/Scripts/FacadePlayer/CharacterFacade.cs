using Characters;
using UnityEngine;

public abstract class CharacterFacade
{
    protected abstract Stats Stats { get; set; }
    public abstract void PerformMovement(float speed);
    public abstract void PerformAttack();
    public abstract void PerformCast();
    public abstract void PerformJump();
    public abstract void PerformRoll();
    public abstract void PerformInteraction();
    public abstract void PerformCameraRotation(Camera camera, Vector2 input, Transform transform);
}