using System;
using DG.Tweening;
using UnityEngine;

namespace Goldberg_Machine.Scripts
{
    public class CarLogic : MonoBehaviour
    {
        [SerializeField] private GameObject _button;
        private ButtonLogic _buttonLogic;

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
            transform.DOMoveX(45, 8, false);
        }
    }
}