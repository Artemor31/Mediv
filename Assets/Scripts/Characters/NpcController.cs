using System;
using UnityEngine;

namespace Characters
{
    public class NpcController : MonoBehaviour, IInteractible
    {
        [SerializeField] private GameObject panel;

        private BoxCollider _collider;
        
        
        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
        }

        public void Interact()
        {
            Debug.Log(this.name);
        }
    }
}