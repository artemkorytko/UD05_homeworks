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
        
        private List<Viking> _vikings = new List<Viking>(10);
        private List<Transform> _points = new List<Transform>(10);

        private void Awake()
        {
            _tower = FindObjectOfType<Tower>();
            _spawnVikings = FindObjectOfType<SpawnViking>();
            _path = FindObjectOfType<Path>();
        }
        
        private void Start()
        {
            _vikings = _spawnVikings.GenereteVikings(_path.transform.childCount);
            FillingListPoints();
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
            //foreach (var point in _points) { } // переделать под этот цикл, чтоб удалять точки 
            for (int i = 0; i < _points.Count; i++)
            {
                _vikings[i].SetTarget(_points[i].position);
            }
        }
        
        private void FillingListPoints()
        {
            for (int i = 0; i < _path.transform.childCount; i++)
            {
                _points.Add(_path.transform.GetChild(i));
            }
        }
    }
}