using UnityEngine;
using Weapons;

namespace Characters
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private Weapon _currentWeapon;

        private AnimatorScheduler _animator;
        private Collider _weaponCollider;
        private Transform _rightHand;
        private Transform _leftHand;

        private float _timeToAttack;


        private void Start()
        {
            _rightHand = transform.GetChild(0).gameObject.GetComponent<PlaceHolder>().rightHand;
            _leftHand = transform.GetChild(0).gameObject.GetComponent<PlaceHolder>().leftHand;
            _animator = transform.GetChild(0).gameObject.GetComponent<AnimatorScheduler>();
            _animator.OnAttackEnded += DisableWeaponCollider;
            _timeToAttack = _currentWeapon.Delay;

            EquipWeapon(_currentWeapon);
            DisableWeaponCollider();
        }

        private void Update()
        {
            _timeToAttack = _timeToAttack > 0 ? _timeToAttack - Time.deltaTime : 0;
        }

        public void Attack()
        {
            //var staminaEnough = _stats.TryConsumeStamina(_currentWeapon.StaminaConsumption);
            //if (_timeToAttack != 0 || !staminaEnough) return;

            _weaponCollider.enabled = true;
            _timeToAttack = _currentWeapon.Delay;
            _animator.Attack();
        }

        public void ApplyDamage(Stats stats)
        {
            stats.TakeDamage(_currentWeapon.Damage);
        }

        public void EquipWeapon(Weapon weapon)
        {
            _currentWeapon = weapon;
            var anim = _animator.GetAnimatorController();
            //var gripType = weapon.Spawn(_rightHand, _leftHand, anim);

           // _weaponCollider = gripType == GripType.RightHand
           //     ? _rightHand.transform.Find("CurrentWeapon").GetChild(0).GetComponent<Collider>()
           //     : _leftHand.transform.Find("CurrentWeapon").GetChild(0).GetComponent<Collider>();
        }
        
        private void DisableWeaponCollider() => _weaponCollider.enabled = false;
    }
}