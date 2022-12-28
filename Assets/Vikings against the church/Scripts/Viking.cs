using System;
using UnityEngine;

namespace Vikings_against_the_church.Scripts
{
    public class Viking : MonoBehaviour
    {
        [SerializeField] private string _name = "None";
        [SerializeField] private float _speed = 3f;
        
        private Vector3 _target;
        private int _reward;
        private Vector3 _firstPoint;

        private void Awake()
        {
            _firstPoint = transform.position; // это будет для возврата на карабль
        }

        private void Update()
        {
            if (transform.position != _target)
                Move();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Tower tower))
                _target = tower.GetTrargetCoin().transform.position;
            

            // if (other.TryGetComponent(out Coin coin))
            // {
            //     _reward += coin.Reward;
            //     _target = _firstPoint;
            //     coin.gameObject.SetActive(false);
            // }
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        }

        public void SetTarget(Vector3 vector3)
        {
            _target = vector3;
        }
    }
}