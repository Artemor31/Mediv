using Characters;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        // weapon name to find it in hierarchy for deleting
        private const string WeaponName = "CurrentWeapon";
        
        public float Damage => _damage;
        public float Delay => _attackDelay;
        public GripType GripType => _gripType;
        
        [SerializeField] protected GameObject _weaponPrefab;
        [SerializeField] protected AnimatorOverrideController _animatorOverrider;
        [SerializeField] protected ImpactEffect _impact;
        [SerializeField] protected GripType _gripType;
        
        [SerializeField] protected float _damage;
        [SerializeField] protected float _attackDelay;

        public void SpawnWeaponInPosition(Transform parent)
        {
            var weapon = Instantiate(_weaponPrefab, parent);
            weapon.name = WeaponName;
        }
    }
}