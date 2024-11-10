using System;
using UnityEngine;

namespace Gameplay.Common
{
    public class TriggerHandler : MonoBehaviour
    {
        public event Action <Collider> OnEnter;
        public event Action <Collider> OnStay;
        public event Action <Collider> OnExit;

        private void OnTriggerEnter(Collider collider)
        {
            OnEnter?.Invoke(collider);
        }

        private void OnTriggerStay(Collider collider)
        {
            OnStay?.Invoke(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            OnExit?.Invoke(collider);
        }
    }
}
