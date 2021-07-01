using System;
using UnityEngine;

public abstract class CharacterAnimator
{
    protected Animator _animator;

    public abstract void Die();
    public abstract void UpdateMoveSpeed(float speed);
    public abstract void Attack();
}

