using System.Collections.Generic;
using Characters;
using UnityEngine;
using Weapons;

public class ProjectileHolder : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    
    public List<GameObject> CreatePool(int size)
    {
        var pool = new List<GameObject>();
        
        for (var i = 0; i < size; i++)
        {
            var projectile = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
            projectile.SetActive(false);
            pool.Add(projectile);
        }
        return pool;
    }
}