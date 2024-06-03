using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Common
{
    public class TriggerHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Collider> onEnter;
        [SerializeField] private UnityEvent<Collider> onStay;
        [SerializeField] private UnityEvent<Collider> onExit;

        public event UnityAction<Collider> OnEnter
        {
            add => onEnter.AddListener(value);
            remove => onEnter.RemoveListener(value);
        }

        public event UnityAction<Collider> OnStay
        {
            add => onStay.AddListener(value);
            remove => onStay.RemoveListener(value);
        }

        public event UnityAction<Collider> OnExit
        {
            add => onExit.AddListener(value);
            remove => onExit.RemoveListener(value);
        }

        private void OnTriggerEnter(Collider collider)
        {
            onEnter?.Invoke(collider);
        }

        private void OnTriggerStay(Collider collider)
        {
            onStay?.Invoke(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            onExit?.Invoke(collider);
        }
    }
}
