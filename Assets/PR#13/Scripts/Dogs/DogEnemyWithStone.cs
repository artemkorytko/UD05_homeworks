using System;
using DG.Tweening;
using DogOOP.Base;
using UnityEngine;
using Weapons;

namespace DogOOP
{
    public class DogEnemyWithStone : DogsBase
    {
        [SerializeField] private Stone prefabStone;
         [SerializeField] private Transform transformThrow;

         private float _angle;
         private float _counter;
         private float _radius;
         
         private Vector3 _startPoint;
         private Vector3 _prefPosition;

         private void Awake()
         {
             base.Awake();
             _radius = configDog.Radius;
         }

         private void Start()
         {
             _startPoint = Vector3.zero - transform.position;
         }

         private void Update()
         {
             _prefPosition -= transform.position;
             _counter += Time.deltaTime;
             _animator.SetFloat(Speed, _prefPosition.magnitude);

             if (!_target)
             {
                 CircleMove();
                 SearchPlayer();
                 return;
             }
             _prefPosition = transform.position;
             
             if(_counter >= _delayBetweenAttack)
             {
                 transform.rotation = Quaternion.LookRotation(_target.transform.position);
                 _animator.SetTrigger(Attack02);
                 _counter = 0;
                 Attack();
             }
         }
         
        private void CircleMove()
        {
            _angle += Time.deltaTime * _speed;
            
            var transformPosition = transform.position;
            
            transformPosition.x =  _radius * Mathf.Sin(_angle);
            transformPosition.z =  _radius * Mathf.Cos(_angle);
            
            transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg *_angle + 90,0);
            transform.position = transformPosition - _startPoint;
          
        }
        
        protected override void Attack()
        {
            var stone = Instantiate(prefabStone.gameObject, transformThrow.position, Quaternion.identity);
            stone.transform.DOJump(_target.transform.position, 3, 1,1).SetEase(Ease.OutQuad);
        }
    }
}