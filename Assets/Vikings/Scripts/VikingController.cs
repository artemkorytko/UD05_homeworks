using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Vikings.Scripts
{
    public class VikingController : MonoBehaviour
    {
        private Animator _animator;
        private LootManager _lootManager;
        private GameManager _gm;
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

        private VikingUiController _UiController;
        private static float _uiShowDelay = 2f;

        private string _name;

        private void Awake()
        {
            _gm = FindObjectOfType<GameManager>();
            _animator = GetComponent<Animator>();
            _UiController = GetComponentInChildren<VikingUiController>();
            _UiController.gameObject.SetActive(false);
            _lootManager = FindObjectOfType<LootManager>();

            var namer = FindObjectOfType<VikingNamer>();
            _name = namer.NameViking();
            GetComponentInChildren<NameDisplay>().SetName(_name);
        }

        public async void TurnAndMove(Vector3 point, float moveDuration, float rotateDuration)
        {
            await transform.DOLookAt(point, rotateDuration, AxisConstraint.None, Vector3.up);
            _animator.SetBool(IsRunning, true);
            await transform.DOMove(point, moveDuration, false).SetEase(Ease.Linear);
            _animator.SetBool(IsRunning,false);
        }

        public void Attack()
        { 
            _animator.SetTrigger(IsAttacking);
        }

        public async void ShowDialogCloud(string text)
        {
            _UiController.gameObject.SetActive(true);
            _UiController.UpdateUi(_name,text);
            await UniTask.Delay(TimeSpan.FromSeconds(_uiShowDelay));
            _UiController.gameObject.SetActive(false);
        }
        
        private void OnTriggerEnter(Collider col)
        {
            if (col.GetComponent<GateComponent>())
            {
                TurnAndMove(col.GetComponent<GateComponent>().lootPoints.Pop(), _gm._vikingMoveDuration, _gm._rotateDuration);
            }
            
            if (col.GetComponent<BarrelComponent>())
            {
                var loot = _lootManager.Find();
                _gm.FindLoot(_name, loot);
                ShowDialogCloud(loot);
            }
        }
    }
}