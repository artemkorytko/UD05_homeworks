using System;
using DG.Tweening;
using UnityEngine;

namespace Goldberg_Machine.Scripts
{
    public class CarLogic : MonoBehaviour
    {
        [SerializeField] private GameObject _button;
        private ButtonLogic _buttonLogic;
        private Vector3 _rotationAxis = new Vector3(0, 0, -1);
        [SerializeField] private float MoveEndPos = 40f;
        [SerializeField] private float MoveDuration = 5f;
        [SerializeField] private float RotateAngle = 90f;
        [SerializeField] private float RotateDuration = 3f;
        

        private void Awake()
        {
            _buttonLogic = _button.GetComponentInChildren<ButtonLogic>();
        }
        
        private void Start()
        {
            _buttonLogic.ButtonHit += StartEngine;
        }

        private void OnDestroy()
        {
            _buttonLogic.ButtonHit -= StartEngine;
        }

        private void StartEngine()
        {
            DOTween.Sequence()
                .Append(transform.DOMoveX(MoveEndPos, MoveDuration, false))
                .Append(transform.DORotate(transform.eulerAngles + Quaternion.AngleAxis(RotateAngle, _rotationAxis).eulerAngles, RotateDuration, RotateMode.Fast));

        }
    }
}