using System;
using Weapons;

public class Attacker : IAttacker
{
    public Weapon Weapon { get; private set; }
    
    public void EquipWeapon(Weapon weapon)
    {
        throw new NotImplementedException();
    }

    public void Attack()
    {
        throw new NotImplementedException();
    }
}