using System;
using Characters;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        // weapon name to find it in hierarchy for deleting
        private const string WeaponName = "CurrentWeapon";
        
        // Model overrides
        [SerializeField] private GameObject weaponPrefab;
        [SerializeField] private AnimatorOverrideController animatorOverrider;
        [SerializeField] private WeaponGripType gripType;
        
        // Stats 
        [SerializeField] private float damage = 10f;
        [SerializeField] private float range = 1f;
        [SerializeField] private float attackDelay = 1f;
        [SerializeField] private float staminaConsumption = 33f;
        
        // Getters.
        public float Damage => damage;
        public float Range => range;
        public float Delay => attackDelay;
        public float StaminaConsumption => staminaConsumption;
        
        /// <summary>
        /// Spawns weapon in given hand(s) and setup this weapon.
        /// </summary>
        /// <param name="rightHand"></param>
        /// <param name="leftHand"></param>
        /// <param name="currentAnimator"></param>
        /// <returns></returns>
        public WeaponGripType Spawn(Transform rightHand, Transform leftHand, Animator currentAnimator)
        {
            DestroyOldWeapon(rightHand, leftHand);
            WeaponGripType type = SpawnWeaponPrefab(rightHand, leftHand);
            
            if (animatorOverrider != null)
                currentAnimator.runtimeAnimatorController = animatorOverrider;
            return type;
        }

        /// <summary>
        /// Creates prefab of weapon in given hand(s).
        /// </summary>
        /// <param name="rightHand"></param>
        /// <param name="leftHand"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private WeaponGripType SpawnWeaponPrefab(Transform rightHand, Transform leftHand)
        {
            switch (gripType)
            {
                case WeaponGripType.RightHand:
                {
                    GameObject weapon = Instantiate(weaponPrefab, rightHand);
                    weapon.name = WeaponName;
                    return gripType;
                }
                case WeaponGripType.LeftHand:
                {
                    GameObject weapon = Instantiate(weaponPrefab, leftHand);
                    weapon.name = WeaponName;
                    return gripType;
                }
                case WeaponGripType.Both:
                {
                    GameObject weapon = Instantiate(weaponPrefab, rightHand);
                    weapon.name = WeaponName;
                    weapon = Instantiate(weaponPrefab, leftHand);
                    weapon.name = WeaponName;
                    return gripType;
                }
                default:
                    throw new Exception("Weapon prefab not found");
            }
        }

        /// <summary>
        /// Destroys current weapon before equip new.
        /// </summary>
        /// <param name="rightHand"></param>
        /// <param name="leftHand"></param>
        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            FindAndDestroyChild(rightHand);
            FindAndDestroyChild(leftHand);
        }

        /// <summary>
        /// Find weapon in had prefab and destroys it.
        /// </summary>
        /// <param name="Hand"></param>
        private void FindAndDestroyChild(Transform Hand)
        {
            var oldWeapon = Hand.Find(WeaponName);
            if (oldWeapon == null) return;
            
            oldWeapon.name = "destroy";
            Destroy(oldWeapon.gameObject);
        }
    }
}
