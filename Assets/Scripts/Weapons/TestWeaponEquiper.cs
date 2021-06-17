using Characters;
using UnityEngine;

namespace Weapons
{
    public class TestWeaponEquiper : MonoBehaviour
    {
        [SerializeField] private Fighter _fighter;
        [SerializeField] private Weapons.Weapon rapier;
        [SerializeField] private Weapons.Weapon sword;

        public void EquipSword()
        {
            _fighter.EquipWeapon(sword);
        }       
        public void EquipRapier()
        {
            _fighter.EquipWeapon(rapier);
        }
    }
}