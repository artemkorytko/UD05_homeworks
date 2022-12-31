using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vikings_against_the_church.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float _delay;
        
        private SpawnVikings _spawnVikingsPoint;
        private Path _path;
        private Tower _tower;
        
        private List<Viking> _listVikings = new List<Viking>(10);
        private Stack<Viking> _stackVikings = new Stack<Viking>(10);
        private Queue<Transform> _points = new Queue<Transform>(10);
        private Dictionary<int, string> _dictionaryRewards = new Dictionary<int, string>();

        private void Awake()
        {
            _tower = FindObjectOfType<Tower>();
            _path = FindObjectOfType<Path>();
            
            _spawnVikingsPoint = FindObjectOfType<SpawnVikings>();
        }
        
        private void Start()
        {
            FillingQueuePoints();
            _spawnVikingsPoint.OnActiveMoveViking += StartMoveVikings;
        }

        private void OnDestroy()
        {
            _spawnVikingsPoint.OnActiveMoveViking -= StartMoveVikings;
        }

        private void FillingQueuePoints()
        {
            for (int i = 0; i < _path.transform.childCount; i++)
                _points.Enqueue(_path.transform.GetChild(i));
        }
        
        private void StartMoveVikings() 
        {
            _listVikings = _spawnVikingsPoint.GenereteVikings(_path.transform.childCount);
            
            foreach (var viking in _listVikings)
                viking.SetTarget(_points.Dequeue().position);
            
            StartCoroutine(Expectation());
        }

        private IEnumerator Expectation()
        {
            yield return new WaitUntil(()=> _listVikings[^1].IsAttackTower); // _listVikings[^1] - берет последний item 
            yield return new WaitForSeconds(_delay);
            
            yield return QueueVikingsAttackTower();
        }

        private IEnumerator QueueVikingsAttackTower()
        {
            var queue = _listVikings.OrderBy(viking => Vector3.Distance(viking.transform.position, _tower.transform.position)).ToList();
            foreach (var viking in queue)
            {
                viking.SetTarget(_tower.transform.position);
                _stackVikings.Push(viking);
                
                yield return new WaitForSeconds(_delay);
            }
            
            yield return new WaitUntil(() => queue[^1].IsReturnInSpawnPiont);
            yield return ReturnVikingsInSpawnPiont();
        }
        
        private IEnumerator ReturnVikingsInSpawnPiont()
        {
            foreach (var viking in _stackVikings)
            {
                _dictionaryRewards.TryAdd(viking.Reward, viking.Name);
              //  Debug.Log(_dictionaryRewards.Count);
                viking.SetTarget(viking.FirstPoint);
                yield return new WaitForSeconds(_delay);
            }
        }
    }
}