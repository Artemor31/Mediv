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
        private Stats _stats;

        /// <summary>
        /// Count of attack animations in Player Animator.
        /// </summary>
        private const int AttackCounter = 3;
        private int _currentAttack = 1;
        private float _timeToAttack;

        
        private void Start()
        {
            _stats = GetComponent<Stats>();
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
            var staminaEnough = _stats.TryConsumeStamina(_currentWeapon.StaminaConsumption);
            if (_timeToAttack != 0 || !staminaEnough) return;
            
            _weaponCollider.enabled = true;
            _timeToAttack = _currentWeapon.Delay;

            UpdateCombo();
            
            _animator.Attack(_currentAttack);
        }

        public void ApplyDamage(Stats stats)
        {
            stats.TakeDamage(_currentWeapon.Damage);
        }

        private void UpdateCombo()
        {
            if (_currentAttack == AttackCounter) 
                _currentAttack = 1;
            else
                _currentAttack++;
        }

        /// <summary>
        /// Spawns new weapon in character arm.
        /// </summary>
        /// <param name="weapon"></param>
        public void EquipWeapon(Weapon weapon)
        {
            _currentWeapon = weapon;
            var anim = _animator.GetAnimatorController();
            var gripType = weapon.Spawn(_rightHand, _leftHand, anim);

            _weaponCollider = gripType == WeaponGripType.RightHand
                ? _rightHand.transform.Find("CurrentWeapon").GetChild(0).GetComponent<Collider>()
                : _leftHand.transform.Find("CurrentWeapon").GetChild(0).GetComponent<Collider>();
        }

        /// <summary>
        /// Disable collider while character not attacking.
        /// </summary>
        private void DisableWeaponCollider() => _weaponCollider.enabled = false;
        
    }
}
