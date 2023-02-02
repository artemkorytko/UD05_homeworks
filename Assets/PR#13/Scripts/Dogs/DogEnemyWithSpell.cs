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
            _rotateSpeed = config.RotateSpeed;
        }

        private void Start()
        {
            MoveRotate();
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

        protected override void DoAction()
        {
            if (!_target)
            {
                SearchPlayer();
                return;
            }
            
            var direction = _target.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
            _counter += Time.deltaTime;
            
            if (_counter >= config.DelayBetweenAttack)
            {
                _counter = 0;
                Attack();
            }

        }
    }
}