using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using DG.Tweening;
using DogOOP.Base;
using UnityEngine;

namespace DogOOP
{
    public class DogEnemyWithSword : DogsBase
    {
        [SerializeField] private float distanceAttack;
        
        private Transform pointPotrul;
        private Vector3 _startPoint;
        private Vector3 _prefPosition;
        private float _counter;
        private void Start()
        {
            pointPotrul = FindObjectOfType<PointPotrul>().transform;
            _startPoint = transform.position;
            Move();
        }

        private void Update()
        {
            _prefPosition -= transform.position;
            _animator.SetFloat(Speed, _prefPosition.magnitude);
            if (!_target)
            {
                SearchPlayer();
                _prefPosition = transform.position;
                return;
            }

            _prefPosition = transform.position;
            
            var distance = Vector3.Distance(transform.position, _target.transform.position);
            _counter += Time.deltaTime;
            if (distanceAttack < distance)
            {
                transform.rotation = Quaternion.LookRotation(_target.transform.position);
                MoveToPlayer();
            }
            else if(_delayBetweenAttack <=_counter)
            {
                _counter = 0;
                Attack();
            }

        }

        private void MoveToPlayer()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }

        private async UniTask Move()
        {
            if(_target) return;
            
            await transform.DOMove(pointPotrul.position, _speed);
            await transform.DORotate(new Vector3(0, 180, 0), 2f);
            await transform.DOMove(_startPoint, 2f);
            await transform.DORotate(new Vector3(0, 0, 0), _speed);
            Move();
        }
        
        protected override void Attack()
        {
            _animator.SetTrigger(Attack02);
        }
    }
}