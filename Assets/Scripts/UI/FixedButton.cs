using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class FixedButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [HideInInspector]
        public bool pressed;

        // UI buttons events
        public event Action OnJumpClicked;
        public event Action OnAttackClicked;
        public event Action OnInteractClicked;

        private void Start()
        {
            name = gameObject.name;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            pressed = true;
            OnAttackClicked?.Invoke();
            OnJumpClicked?.Invoke();
            OnInteractClicked?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            pressed = false;
        }
    }
}