using System;

public class MobileActions : IAttacking, IInteracting, IJumping 
{
    public event Action Attacked;
    public event Action Interacted;
    public event Action Jumped;
}