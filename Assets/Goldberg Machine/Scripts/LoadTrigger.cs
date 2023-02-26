using System;
using UnityEngine;

namespace Goldberg_Machine.Scripts
{
    public class LoadTrigger : MonoBehaviour
    {
        public event Action CannonLoad;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<TriggerObject>())
            {
                CannonLoad?.Invoke();
            }
        }
    }
}