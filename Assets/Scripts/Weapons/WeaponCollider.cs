using Characters;
using UnityEngine;

namespace Weapons
{
    public class WeaponCollider : MonoBehaviour
    {
        private Fighter _character;
        private void OnTriggerEnter(Collider other)
        {
            if (TryGetComponent<Stats>(out var stats))
                _character.ApplyDamage(stats);
        }
    }
}
