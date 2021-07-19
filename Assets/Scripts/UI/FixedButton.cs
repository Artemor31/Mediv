using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class FixedButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [HideInInspector]
        public bool pressed;

        public event Action OnAttacked;
        public event Action OnInteracted;
        public event Action OnJumped;
        public event Action OnCasted;
        
        private void Start()
        {
            name = gameObject.name;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            pressed = true;
            OnAttacked?.Invoke();
            OnJumped?.Invoke();
            OnRolled?.Invoke();
            OnInteracted?.Invoke();
            OnCasted?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            pressed = false;
        }

        public event Action OnRolled;
    }
}