using UnityEngine;
using Weapons;

[CreateAssetMenu(menuName = "Create MeleeWeapon", fileName = "MeleeWeapon", order = 0)]
public class MeleeWeapon : Weapon
{
    public float StaminaConsumption => _staminaConsumption;
    
    [SerializeField] protected float _staminaConsumption;
    
    private Collider _collider;

    public void SetColliderEnable(bool state)
    {
        _collider.enabled = state;
    }
    
}