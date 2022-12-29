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
            _spawnVikingsPoint = FindObjectOfType<SpawnVikings>();
            _path = FindObjectOfType<Path>();
        }
        
        private void Start()
        {
            _listVikings = _spawnVikingsPoint.GenereteVikings(_path.transform.childCount);
            FillingQueuePoints();
            StartMoveVikings();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                StartCoroutine(QueueVikingsAttackTower());

            if (Input.GetMouseButtonDown(1))
                StartCoroutine(ReturnVikingsInSpawnPiont());
        }
        

        private IEnumerator QueueVikingsAttackTower()
        {
            var queue = _listVikings.OrderBy(viking => Vector3.Distance(viking.transform.position, _tower.transform.position));
            foreach (var viking in queue)
            {
                viking.SetTarget(_tower.transform.position);
                _stackVikings.Push(viking);
                yield return new WaitForSeconds(_delay);
            }
        }

        private void StartMoveVikings() 
        {
            foreach (var viking in _listVikings)
            {
                viking.SetTarget(_points.Dequeue().position);
            }
        }

        private IEnumerator ReturnVikingsInSpawnPiont()
        {
            foreach (var viking in _stackVikings)
            {
                _dictionaryRewards.TryAdd(viking.Reward, viking.Name);
                //Debug.Log(_dictionaryRewards.Count);
                viking.SetTarget(viking.FirstPoint);
                yield return new WaitForSeconds(_delay);
            }
        }

        private void FillingQueuePoints()
        {
            for (int i = 0; i < _path.transform.childCount; i++)
            {
                _points.Enqueue(_path.transform.GetChild(i));
            }
        }
    }
}