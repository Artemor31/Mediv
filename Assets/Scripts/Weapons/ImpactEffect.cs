using System;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _particle.Play();
    }

    private void OnDisable()
    {
        _particle.Stop();
    }

    public void ShowImpact(Collider other)
    {
        transform.position = GetColliderCenter(other);
        gameObject.SetActive(true);
    }

    private Vector3 GetColliderCenter(Collider other)
    {
        var capsuleCollider = other.GetComponent<CapsuleCollider>();
        return capsuleCollider == null 
            ? other.transform.position 
            : capsuleCollider.center;
    }
}