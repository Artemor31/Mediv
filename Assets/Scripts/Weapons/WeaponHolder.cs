using System;
using Characters;
using UnityEngine;
using Weapons;

public class WeaponHolder : MonoBehaviour
{
    private const string WeaponName = "CurrentWeapon";

    [SerializeField] private MeleeWeapon _meleeWeapon;
    [SerializeField] private RangeWeapon _rangeWeapon; 

    private readonly Transform _rightHand;
    private readonly Transform _leftHand;

    private Weapon _currentWeapon;
    private CurrentWeapon _currentType;

    private void OnEnable()
    {
        SpawnWeapon(_meleeWeapon);
        _currentWeapon = _meleeWeapon;
        _currentType = CurrentWeapon.Melee;
    }

    public void Swap()
    {
        SwapCurrentType();
        DestroyOldWeapon(_rightHand, _leftHand);
        SpawnNewWeapon();
    }

    private void SwapCurrentType()
    {
        _currentType = _currentType == CurrentWeapon.Melee ? CurrentWeapon.Range : CurrentWeapon.Melee;
    }

    private void SpawnNewWeapon()
    {
        if (_currentWeapon == _meleeWeapon)
            SpawnWeapon(_rangeWeapon);
        else
            SpawnWeapon(_meleeWeapon);
    }

    private void SpawnWeapon(Weapon weapon)
    {
        switch (weapon.GripType)
        {
            case GripType.RightHand:
                weapon.SpawnWeaponInPosition(_rightHand);
                break;
            
            case GripType.LeftHand:
                weapon.SpawnWeaponInPosition(_leftHand);
                break;
            
            case GripType.Both:
                weapon.SpawnWeaponInPosition(_rightHand);
                weapon.SpawnWeaponInPosition(_leftHand);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    // Destroys current weapon before equip new.
    private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
    {
        FindAndDestroyChild(rightHand);
        FindAndDestroyChild(leftHand);
    }
        
    private void FindAndDestroyChild(Transform hand)
    {
        var oldWeapon = hand.Find(WeaponName);
        if (oldWeapon == null) return;
            
        oldWeapon.name = "destroy";
        Destroy(oldWeapon.gameObject);
    }

    private enum CurrentWeapon
    {
        Melee,
        Range
    }
}