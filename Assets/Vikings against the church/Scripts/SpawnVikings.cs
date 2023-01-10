using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Vikings_against_the_church.Scripts.Viking2._0;
using Random = UnityEngine.Random;

namespace Vikings_against_the_church.Scripts
{
    public class SpawnVikings : MonoBehaviour
    {
        [SerializeField] private List<VikingController> _vikings;
        [SerializeField] private float _speedShip = 1f;

        private bool _isMove = true;
        
        public event Action OnActiveMoveViking;
        
        private void Update()
        {
            if (_isMove)
                Move();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Background>())
            {
                OnActiveMoveViking?.Invoke();
                _isMove = false;
            }
        }

        private void Move()
        {
            transform.Translate(Vector3.forward * (_speedShip * Time.deltaTime));
        }

        public List<VikingController> GenereteVikings(int count)
        {
            List<VikingController> vikings = new List<VikingController>(count);

            for (int i = 0; i < count; i++)
            {
                var randomViking = Random.Range(0, _vikings.Count);
                var viking = Instantiate(_vikings[randomViking].gameObject,transform.position, Quaternion.identity).GetComponent<VikingController>();
                vikings.Add(viking);
            }
            return vikings;
        }
    }
}