using System;
using System.Collections;
using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.UI;

namespace Characters
{
    public class Stats : MonoBehaviour
    {
        private const float RemoveAfterDeathTime = 5;
        
        [SerializeField] private AnimatorScheduler animator;
        
        [SerializeField] private float health;
        [SerializeField] private float stamina;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _maxStamina;
        [SerializeField] private float _money = 0;

        [SerializeField] private Image _staminaUI;
        
        private bool _dead;
        
        private void Awake()
        {
            Application.targetFrameRate = 200;
            animator = transform.GetChild(0).gameObject.GetComponent<AnimatorScheduler>();
        }

        private void Start()
        {
            var buffer = SaveGame.Load<float>("stam");
            stamina = buffer;
        }

        private void FixedUpdate()
        {
            if (!(stamina < 100)) return;
            
            stamina += 3 * Time.deltaTime;
            _staminaUI.fillAmount = stamina / 100;
        }

        /// <summary>
        /// Tries consume stamina before attack.
        /// </summary>
        /// <param name="staminaCost"></param>
        /// <returns></returns>
        public bool TryConsumeStamina(float staminaCost)
        {
            if (!(stamina >= staminaCost)) return false;
            stamina -= staminaCost;
            return true;
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health -= damage, 0);
            if (health == 0)  Die();
        }

        private void Die()
        {
            _dead = true;
            GetComponent<Collider>().enabled = false;
            animator.Die();
            StartCoroutine(RemoveAfterDeath());
        }

        /// <summary>
        /// Removes dead body after death;
        /// </summary>
        /// <returns></returns>
        private IEnumerator RemoveAfterDeath()
        {
            yield return new WaitForSeconds(RemoveAfterDeathTime);
            Destroy(gameObject);
        }

        public bool IsDead() => _dead;

        
        private void OnApplicationQuit() => SaveGame.Save("stam", stamina);
        private void OnApplicationPause(bool pauseStatus) => SaveGame.Save("stam", stamina);
    }
}
