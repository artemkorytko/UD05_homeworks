using System;
using UnityEngine;

namespace Scripts.PR
{
    public class Phase2 : MonoBehaviour
    {
        [SerializeField] private Transform hammer;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Bullet>())
            {
                
            }
        }
    }
}