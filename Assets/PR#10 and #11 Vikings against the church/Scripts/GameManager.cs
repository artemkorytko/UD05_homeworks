using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Vikings_against_the_church.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float delay;
        [SerializeField] private Transform targetShip;
        [SerializeField] private Transform returnShip;
        [SerializeField] private float durationMoveShip;
        
        private ShipSpawnVikings _shipSpawnVikingsPoint;
        private Path _path;
        private Tower _tower;

        private List<VikingController> _listVikings = new List<VikingController>(10);
        private Stack<VikingController> _stackVikings = new Stack<VikingController>(10);
        private Queue<Transform> _points = new Queue<Transform>(10);
        private Dictionary<int, string> _dictionaryRewards = new Dictionary<int, string>(10);

        private void Awake()
        {
            _tower = FindObjectOfType<Tower>();
            _path = FindObjectOfType<Path>();
            _shipSpawnVikingsPoint = FindObjectOfType<ShipSpawnVikings>();
        }
        
        private async void Start()
        {
            FillingQueuePoints();
            
            await _shipSpawnVikingsPoint.transform.DOMove(targetShip.position, durationMoveShip); 
            MoveVikingsToPoint();
            await Expectation();
            await QueueVikingsAttackTower();
            await ReturnVikingsInSpawnPiont();
            await ShipIsSailing();
        }

        private async UniTask ShipIsSailing()
        {
            await UniTask.WaitUntil(() => _listVikings[0].transform.position == _shipSpawnVikingsPoint.transform.position);
            _shipSpawnVikingsPoint.transform.DOMove(returnShip.position, durationMoveShip);
        }

        private void FillingQueuePoints()
        {
            for (int i = 0; i < _path.transform.childCount; i++)
                _points.Enqueue(_path.transform.GetChild(i));
        }
        
        private void MoveVikingsToPoint() 
        {
            _listVikings = _shipSpawnVikingsPoint.GenereteVikings(_path.transform.childCount);
            
            foreach (var viking in _listVikings)
                viking.SetTarget(_points.Dequeue());
        }

        private async UniTask Expectation()
        {
            await UniTask.WaitUntil(()=> _listVikings[0].IsAttackTower);
        }

        private async UniTask QueueVikingsAttackTower()
        {
            var queue = _listVikings.OrderBy(viking => Vector3.Distance(viking.transform.position, _tower.transform.position)).ToList();
            foreach (var viking in queue)
            {
                viking.SetTarget(_tower.transform);
                _stackVikings.Push(viking);
                
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
            }
            
            await UniTask.WaitUntil(() => queue[^1].IsReturnInSpawnPiont);// [^1] - берет последний item 
            
        }
        
        private async UniTask ReturnVikingsInSpawnPiont()
        {
            foreach (var viking in _stackVikings)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
                
                _dictionaryRewards.TryAdd(viking.Reward, viking.Name);
                viking.SetTarget(_shipSpawnVikingsPoint.transform);
            }
        }
    }
}