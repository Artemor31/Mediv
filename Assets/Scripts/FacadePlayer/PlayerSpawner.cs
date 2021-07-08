using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPoint;

    public void Spawn(GameObject prefab)
    {
        Instantiate(prefab, _spawnPoint.transform.position, Quaternion.identity);
    }
}