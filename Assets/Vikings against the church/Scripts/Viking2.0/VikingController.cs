using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Vikings_against_the_church.Scripts.Viking2._0
{
    public class VikingController : MonoBehaviour
    {
        [SerializeField] private string _name = "None";
        [SerializeField] private Transform _model;
        [SerializeField] private float _speed;
        [SerializeField] private float _speedLerp;
        [SerializeField] private float _delay;

        // [SerializeField] private Transform _transformTarget;
        
        
        private Transform _target;
        private Transform _firstPoint;
        private Quaternion _lookRotation;
        
        private Animator _animator;
        
        private int _reward;
        private bool _isReturnInSpawnPiont;
        private bool _isAttackTower;
        
        public string Name => _name;
        public int Reward => _reward;
        public bool IsReturnInSpawnPiont => _isReturnInSpawnPiont;
        public bool IsAttackTower => _isAttackTower;

        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }
        
        private void Update()
        {
            if (transform.position == _target.position)
                return;
            
            Rotate();
            Move();
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Point>() && Mathf.Abs(transform.position.z - _target.position.z) < 0.3f) 
            {
                _isAttackTower = true;
                _animator.SetTrigger(Idle);
            }

            if (other.TryGetComponent(out Tower tower))
            {
                _animator.SetTrigger(Attack);
                StartCoroutine(AttackTower(tower.GetTrargetCoin().transform));
            }
            
            if (other.TryGetComponent(out Coin coin) && Mathf.Abs(transform.position.z - _target.position.z) < 0.3f)
            {
                _reward = coin.Reward;
                _isReturnInSpawnPiont = true;
                _animator.SetTrigger(Idle);
            }
        }

        private IEnumerator AttackTower(Transform target)
        {
            _target = transform;
            yield return new WaitForSeconds(_delay);
            SetTarget(target);
        }

        public void SetTarget(Transform transformTarget)
        {
            _target = transformTarget;
            _animator.SetTrigger(Walk);
        }

        private void Move()
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }

        private void Rotate()
        {
            //   моментальный поворот // 
            // var relativePos = _target.position - _model.position; // определяем направление поворота 
            // var rotate = Quaternion.LookRotation(relativePos); // Вычисляем Quaternion 
            // _model.localRotation = Quaternion.Euler(rotate.eulerAngles); // устанавливаем Quaternion и перед этим переводим его в угол euler

            // плавный поворот //
            var relativePos = _target.position - _model.position; // определяем направление поворота 
            var rotation = Quaternion.LookRotation(relativePos, Vector3.up);// Вычисляем Quaternion, Vector3.up - это вокруг какой оси делать действие (это ось Y)
            var quaternion = Quaternion.Lerp(_model.localRotation, rotation,_speedLerp * Time.deltaTime); // Плавный поворот от Quaternion до Вычисляем Quaternion   
            _model.localRotation = Quaternion.Euler(quaternion.eulerAngles); // устанавливаем Quaternion и перед этим переводим его в угол euler
            
                // при присвоении в модель нужно Quaternion перреводить в eulerAngles!!!!!!!!
        }
    }
}