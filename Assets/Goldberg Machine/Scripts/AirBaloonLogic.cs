using DG.Tweening;
using UnityEngine;

namespace Goldberg_Machine.Scripts
{
    public class AirBaloonLogic : MonoBehaviour
    {
        [SerializeField] private GameObject _button;
        private ButtonLogic _buttonLogic;
        private Vector3 _rotationAxis = new Vector3(0, 0, -1);
        [SerializeField] private float FlightDuration = 8f;
        [SerializeField] private float FlightEndHeight = -5f;
        [SerializeField] private float RotateDuration = 3f;
        [SerializeField] private float RotateAngle = 45f;

        private void Awake()
        {
            _buttonLogic = _button.GetComponentInChildren<ButtonLogic>();
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
            DOTween.Sequence()
                .Append(transform.DOMoveY(FlightEndHeight, FlightDuration, false))
                .Append(transform.DORotate(transform.eulerAngles + Quaternion.AngleAxis(RotateAngle, _rotationAxis).eulerAngles, RotateDuration, RotateMode.Fast));
        }
    }
}