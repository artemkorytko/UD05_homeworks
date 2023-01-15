using UnityEngine;

namespace Goldberg_Machine.Scripts
{
    public class CannonLogic : MonoBehaviour
    {
        [SerializeField] private GameObject _button;
        private ButtonLogic _buttonLogic;
        private Rigidbody _rig;
        [SerializeField] private float _acceleration;

        private void Awake()
        {
            _buttonLogic = _button.GetComponentInChildren<ButtonLogic>();
            _rig = gameObject.GetComponentInChildren<Rigidbody>();
        }

        private void Start()
        {
            _buttonLogic.ButtonHit += CannonFire;
        }

        private void OnDestroy()
        {
            _buttonLogic.ButtonHit -= CannonFire;
        }

        private void CannonFire()
        {
            _rig.AddForce(Vector3.up.normalized * _acceleration);
        }
    }
}