﻿using UnityEngine;
using UnityEngine.AI;
using Weapons;

namespace Characters
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private AnimatorScheduler animator;
        [SerializeField] private GameObject player;
        [SerializeField] private Weapon weapon;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Stats stats;
        
        [SerializeField] private float moveSpeed;
        [SerializeField] private float weaponRange;

        private void Start()
        {
            weaponRange = weapon.Range;
            weaponRange = 1.5f;
            animator = transform.GetChild(0).gameObject.GetComponent<AnimatorScheduler>();
            player = GameObject.FindGameObjectWithTag("Player");
            agent = GetComponent<NavMeshAgent>();

            agent.speed = moveSpeed;
            ChangeAgentSpeed(moveSpeed);
        }

        private void Update()
        {
            if (stats.IsDead()) return;
            
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                return;
            }
            
            UpdateEnemyState();
        }


        private void UpdateEnemyState()
        {
            if (!InAttackRange())
            {
                MoveTo(player.transform.position);
            }
            else
            {
                StopMovement();
                Attack();
            }
        }
        
        private void Attack()
        {
            animator.Attack(1);
        }

        /// <summary>
        /// Moves to given position.
        /// </summary>
        /// <param name="destination"></param>
        private void MoveTo(Vector3 destination)
        {
            UpdateAnimator();
            agent.destination = destination;
            agent.isStopped = false;
        }

        private void StopMovement()
        {
            agent.isStopped = true;
        }

        /// <summary>
        /// Is player in attack range of character.
        /// </summary>
        /// <returns></returns>
        private bool InAttackRange()
        {
            return Vector3.Distance(transform.position, player.transform.position) < weaponRange;
        }

        private void ChangeAgentSpeed(float speed)
        {
            agent.speed = speed;
        }

        /// <summary>
        /// Updates animator state based on movement speed.
        /// </summary>
        private void UpdateAnimator()
        {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.UpdateMoveSpeed(speed);
        }
    }
}
