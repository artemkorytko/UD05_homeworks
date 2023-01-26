using Cysharp.Threading.Tasks;
using DG.Tweening;
using DogOOP.Base;
using UnityEngine;
using Weapons;

namespace DogOOP
{
    public class DogEnemyWithSpell : DogsBase
    {
        [SerializeField] private float angleLookRotate;
        [SerializeField] private Spell spellPrefab;

        private float _counter;
        private float _rotateSpeed;

        private void Awake()
        {
            base.Awake();
            _rotateSpeed = configDog.RotateSpeed;
        }

        private void Start()
        {
            MoveRotate();
        }
        
        private void Update()
        {
            if (!_target)
            {
                SearchPlayer();
                return;
            }
            
            transform.rotation = Quaternion.LookRotation(_target.transform.position);
            _counter += Time.deltaTime;
            if (_counter >= _delayBetweenAttack)
            {
                Attack();
                _counter = 0;
            }

        }
        
        private async UniTask MoveRotate()
        {
            if(_target) return;
            
            await transform.DORotate(new Vector3(0, angleLookRotate, 0), _rotateSpeed);
            await transform.DORotate(new Vector3(0, -angleLookRotate, 0), _rotateSpeed);
            MoveRotate();
        }
        
        protected override void Attack()
        {
            _animator.SetTrigger(Attack01);
            
            var spell = Instantiate(spellPrefab.gameObject, transform.position + transform.forward,
                Quaternion.identity);
            spell.transform.DOMove(_target.transform.position, 2);
        }
    }
}