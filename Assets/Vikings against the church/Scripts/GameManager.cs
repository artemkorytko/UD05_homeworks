using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vikings_against_the_church.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float _delay;
        
        private SpawnViking _spawnVikings;
        private Path _path;
        private Tower _tower;
        
        private Stack<Viking> _vikings = new Stack<Viking>(10);
        private Queue<Transform> _points = new Queue<Transform>(10);

        private void Awake()
        {
            _tower = FindObjectOfType<Tower>();
            _spawnVikings = FindObjectOfType<SpawnViking>();
            _path = FindObjectOfType<Path>();
        }
        
        private void Start()
        {
            _vikings = _spawnVikings.GenereteVikings(_path.transform.childCount);
            FillingQueuePoints();
            StartMoveVikings();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                StartCoroutine(QueueVikingsAttackTower());
        }
        

        private IEnumerator QueueVikingsAttackTower()
        {
            var queue = _vikings.OrderBy(viking => Vector3.Distance(viking.transform.position, _tower.transform.position));
            foreach (var viking in queue)
            {
                viking.SetTarget(_tower.transform.position);
                yield return new WaitForSeconds(_delay);
            }
        }

        private void StartMoveVikings() 
        {
            foreach (var viking in _vikings)
            {
                viking.SetTarget(_points.Dequeue().position);
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