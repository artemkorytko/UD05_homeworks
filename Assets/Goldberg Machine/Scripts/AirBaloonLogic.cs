using DG.Tweening;
using UnityEngine;

namespace Goldberg_Machine.Scripts
{
    public class AirBaloonLogic : MonoBehaviour
    {
        [SerializeField] private GameObject _button;
        private ButtonLogic _buttonLogic;
        private Rigidbody _rig;

        private void Awake()
        {
            _buttonLogic = _button.GetComponentInChildren<ButtonLogic>();
            _rig = gameObject.GetComponent<Rigidbody>();
        }
        
        private void Start()
        {
            _buttonLogic.ButtonHit += Fly;
        }

        private void OnDestroy()
        {
            _buttonLogic.ButtonHit -= Fly;
        }

        private void Fly()
        {
            _rig.isKinematic = false;
            DOTween.Sequence()
                .Append(transform.DOMoveY(-45, 8, false))
                .Append(transform.DOMoveY(-5, 10, false));
        }
    }
}