using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Vikings.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _npcPrefab;
        private List<VikingController> _vikings = new List<VikingController>();
        private Stack<VikingController> _vikingsStack = new Stack<VikingController>();
        private List<GameObject> _enemyPoints = new List<GameObject>();
        private  List<EnemyComponent> _enemies = new List<EnemyComponent>();
        public Dictionary<string, string> VikingsLoot = new Dictionary<string, string>();

        private Vector3 _spawnPoint;
        private DraccarController _draccarController;
        private Vector3 _draccarStopPoint;
        private GateComponent _gateComponent;
        private Vector3 _gatePoint;
        private UiController _ui;
        private CameraController _camControl;

        [SerializeField] private float _draccarMoveDuration = 5f;
        [SerializeField] public float _rotateDuration = 0.5f;
        [SerializeField] public float _vikingMoveDuration = 3f;
        [SerializeField] private float _vikingActionDelay = 1f;
        [SerializeField] private float _uiShowDelay = 1f;

        private int _vikingsWithLoot = 0;
        private bool _vikingsOnBoard = false;

        private void Awake()
        {
            _ui = FindObjectOfType<UiController>();
            _ui.gameObject.SetActive(false);
            _camControl = FindObjectOfType<CameraController>();
            _draccarController = FindObjectOfType<DraccarController>();
            _draccarStopPoint= (FindObjectOfType<StopPointComponent>()).gameObject.transform.position;
            _spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform.position;
            _gateComponent = FindObjectOfType<GateComponent>();
            _gatePoint = _gateComponent.gameObject.transform.position;

            foreach (var i in FindObjectsOfType<EnemyComponent>())
            {
                _enemyPoints.Add(i.gameObject);
                _enemies.Add(i);
            }
        }

        private void Start()
        {
            DoActionsWithDelay();
        }

        private async void DoActionsWithDelay()
        {
            _camControl.GoToPointOne();
            _draccarController.Arrival(_draccarStopPoint, _draccarMoveDuration);
            await UniTask.WaitUntil(() => _draccarController.gameObject.transform.position == _draccarStopPoint);
            
            VikingSpawn();
            
            GoToBattle();
            await UniTask.Delay(TimeSpan.FromSeconds(_vikingMoveDuration + _rotateDuration));
            
            VikingAttack();
            await UniTask.Delay(TimeSpan.FromSeconds(_vikingActionDelay));
            
            _camControl.GoToPointTwo();
            GoToGate();
            
            await UniTask.Delay(TimeSpan.FromSeconds(_vikingMoveDuration + _rotateDuration));
            
            await UniTask.WaitUntil(() => _vikingsWithLoot == _vikings.Count);
            await UniTask.Delay(TimeSpan.FromSeconds(_vikingMoveDuration + _rotateDuration));
            
            BackToGate();
            await UniTask.Delay(TimeSpan.FromSeconds(_vikingMoveDuration + _rotateDuration));
            
            _camControl.GoToPointOne();
            BackToDraccar();
            await UniTask.WaitUntil(() => _vikingsOnBoard);
            
            _draccarController.Departure(_draccarMoveDuration);
            _ui.gameObject.SetActive(true);
            _ui.ShowStats(_uiShowDelay);
        }

        private Vector3 GetRandomEnemyPoint()
        {
            var r = Random.Range(0, _enemyPoints.Count);
            var point = _enemyPoints[r].transform.position;
            _enemyPoints.RemoveAt(r);
            return point;
        }

        private void VikingSpawn()
        {
            foreach (var enemyPoint in _enemyPoints)
            {
                var r = Random.Range(0, _npcPrefab.Length);
                _vikings.Add((Instantiate(_npcPrefab[r], _spawnPoint,
                    Quaternion.identity, transform)).GetComponent<VikingController>());
            }
        }

        private void GoToBattle()
        {
            foreach (var viking in _vikings)
            {
                var point = GetRandomEnemyPoint();
                viking.TurnAndMove(point, _vikingMoveDuration, _rotateDuration);
            }
        }

        private async void VikingAttack()
        {
            foreach (var viking in _vikings)
            {
                viking.Attack();
            }

            await UniTask.Delay(TimeSpan.FromSeconds(0.4f));

            foreach (var enemy in _enemies)
            {
                enemy.Die();
            }
        }

        private async void GoToGate()
        {
            var sortVikings = _vikings.OrderBy(viking => 
                (_gatePoint - viking.gameObject.transform.position).sqrMagnitude).ToArray();

            _vikings.Clear();
            _vikings = sortVikings.ToList();

            foreach (var viking in _vikings)
            {
                viking.TurnAndMove(_gatePoint, _vikingMoveDuration, _rotateDuration);
                _vikingsStack.Push(viking);
                await UniTask.Delay(TimeSpan.FromSeconds(_vikingActionDelay));
            }
        }

        public void FindLoot(string vikName, string loot)
        {
            _vikingsWithLoot++;
            VikingsLoot.Add(vikName, loot);
        }

        private async void BackToGate()
        {
            foreach (var viking in _vikingsStack)
            {
                viking.TurnAndMove(_gatePoint, _vikingMoveDuration, _rotateDuration);
                await UniTask.Delay(TimeSpan.FromSeconds(_vikingActionDelay));
            }
        }

        private async void BackToDraccar()
        {
            foreach (var viking in _vikingsStack)
            {
                viking.TurnAndMove(_spawnPoint, _vikingMoveDuration, _rotateDuration);
                await UniTask.Delay(TimeSpan.FromSeconds(_vikingActionDelay));
            }
            
            await UniTask.Delay(TimeSpan.FromSeconds(_vikingMoveDuration + _rotateDuration));
            
            foreach (var viking in _vikingsStack)
            {
                Destroy(viking.gameObject);
            }

            _vikingsOnBoard = true;
        }
    }
}
