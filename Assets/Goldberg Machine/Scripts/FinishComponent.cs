using System;
using System.Collections.Generic;
using UnityEngine;

namespace Goldberg_Machine.Scripts
{
    public class FinishComponent : MonoBehaviour
    {
        private List<ConfettiComponent> confCanons = new List<ConfettiComponent>();
        private bool singleShot = true;

        private void Awake()
        {
            foreach (var obj in FindObjectsOfType<ConfettiComponent>())
            {
                confCanons.Add(obj);
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.GetComponent<TriggerObject>())
            {
                if (singleShot)
                {
                    foreach (var obj in confCanons)
                    {
                        obj.ConfettiBurst();
                    }
                }

                singleShot = false;
            }
        }
    }
}