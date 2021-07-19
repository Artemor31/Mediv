using System.Collections.Generic;
using System.Linq;
using Characters;
using UnityEngine;
using Weapons;

[CreateAssetMenu(menuName = "Create RangeWeapon", fileName = "RangeWeapon", order = 0)]
public class RangeWeapon : Weapon
{
    private const int PoolSize = 10;
    public float Range => _range;
    
    [SerializeField] private float _range = 1f;
    [SerializeField] private ProjectileHolder _projectileHolder;

    private Vector3 WeaponPosition => _weaponPrefab.gameObject.transform.position;
    private List<GameObject> _projectilesPool;
    private Stats _target;

    private void OnEnable()
    {
        _projectilesPool = _projectileHolder.CreatePool(PoolSize);
    }

    public void Shoot()
    {
        var projectile = _projectilesPool.FirstOrDefault(p => p.activeInHierarchy == false);
        if (projectile == null) return;

        projectile.transform.position = WeaponPosition;
        projectile.SetActive(true);
    }

    public void SwapTarget()
    {
        var enemies = Physics.OverlapSphere(WeaponPosition, _range)
            .Where(e => e.GetComponent<EnemyController>() != null)
            .Select(e => e.GetComponent<Stats>()).ToArray();
        
        if (enemies.Length == 0) return;
        if (enemies.Length == 1)
        {
            _target = enemies[0];
            return;
        }

        foreach (var enemy in enemies)
        {
            if (enemy == _target) continue;

            var distanceToEnemy = Vector3.Distance(enemy.transform.position, WeaponPosition);
            var distanceToTarget = Vector3.Distance(_target.transform.position, WeaponPosition);
            
            if (distanceToEnemy < distanceToTarget)
            {
                _target = enemy;
                return;
            }
        }

        _target = enemies.FirstOrDefault(e => e != _target);
    }
}