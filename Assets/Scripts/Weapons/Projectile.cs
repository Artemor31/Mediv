using System;
using Characters;
using UnityEngine;
using Weapons;

[RequireComponent(typeof(Collider))]  
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Weapon _weapon;
    private Vector3 _target;

    private void Update()
    {
        if (transform.position == _target) return;
        
        transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var stats = other.GetComponent<Stats>();
        if (stats == null) return;
        
        stats.TakeDamage(_weapon.Damage);
    }
}