using System;
using System.Collections;
using UnityEngine;

namespace Goldberg_Machine.Scripts
{
    public class CannonLogic : MonoBehaviour
    {
        private Rigidbody _rig;
        [SerializeField] private LoadTrigger _loadTrigger;
        [SerializeField] private float FirePower;
        private Vector3 _fireDirection = Vector3.zero;
        private Vector3 _point1 = Vector3.zero;
        [SerializeField] private float FireDelay = 3f;
        private float _resetDelay = 0.02f;

        private void Awake()
        {
            _rig = gameObject.GetComponentInChildren<Rigidbody>();
        }

        private void Start()
        {
             _point1 = _rig.gameObject.transform.position;
             _loadTrigger.CannonLoad += CannonLoaded;
        }

        private void OnDestroy()
        {
            _loadTrigger.CannonLoad -= CannonLoaded;
        }

        private void CannonLoaded()
        {
            StartCoroutine(FireWithDelay());
        }

        private IEnumerator FireWithDelay()
        {
            yield return new WaitForSeconds(FireDelay);
            CannonFire();
        }

        private void CannonFire()
        {
            _fireDirection = _point1 - _rig.gameObject.transform.position;
            _rig.AddForce(_fireDirection.normalized * FirePower);
            StartCoroutine(ResetEnforcer());
        }
        
        private IEnumerator ResetEnforcer()
        {
            yield return new WaitForSeconds(_resetDelay);
            _rig.isKinematic = true;
            _rig.gameObject.transform.position = _point1;
            _rig.isKinematic = false;
        }
    }
}