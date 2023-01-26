using System;
using Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Weapons;
using Weapons.Base;


namespace DogOOP.Base
{
    public abstract class DogsBase : MonoBehaviour
    {
        [SerializeField] protected ConfigDog configDog;
        
        protected int _health;
        protected float _speed;
        protected float _delayBetweenAttack = 2f;
        
        protected Animator _animator;
        protected PlayerController _target;
        
        protected static readonly int Speed = Animator.StringToHash("Speed");
        protected static readonly int Die = Animator.StringToHash("Die");
        protected static readonly int GetHit = Animator.StringToHash("GetHit");
        protected static readonly int Attack01 = Animator.StringToHash("Attack01");
        protected static readonly int Attack02 = Animator.StringToHash("Attack02");
        
        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            
            _health = configDog.Health;
            _speed = configDog.Speed;
            _delayBetweenAttack = configDog.DelayBetweenAttack;
        }
        
        private void ApplayDamage(int damage)
        {
            _health -= damage;
            
            if(_health <= 0)
                Died();
            
            _animator.SetTrigger(GetHit);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out WeaponBase weapon))
            {
                switch (weapon)
                {
                    case Sword:
                        ApplayDamage(weapon.Damage);
                        break;
                    
                    default:
                        ApplayDamage(weapon.Damage);
                        Destroy(weapon.gameObject);
                        break;
                }
            }
        }
        
        private void Died()
        {
            _animator.SetTrigger(Die);
            Rip();
        }
        private async UniTask Rip()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            await transform.DOMoveY(-0.3f, 1f);
            Destroy(gameObject);
        }
        
        protected void SearchPlayer()
        {
            Ray ray = new Ray(transform.position + new Vector3(0, 0.5f,0), transform.forward);
            RaycastHit hit;
            Debug.DrawRay(transform.position + new Vector3(0, 0.5f,0), transform.forward * 15f);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out PlayerController playerController))
                    _target = playerController;
            }
        }
        
        protected abstract void Attack();
        
    }
}