using System;
using UnityEngine;

public interface IPlayerActions : IAttacking, IInteracting, IJumping, ICasting
{
    event Action OnRolled;
}