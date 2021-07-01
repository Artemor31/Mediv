using Characters;

public abstract class CharacterFacade
{
    protected abstract Stats Stats { get; set; }
    public abstract void PerformMovement();
    public abstract void PerformAttack();
}