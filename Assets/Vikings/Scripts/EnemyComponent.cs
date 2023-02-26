using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Vikings.Scripts
{
    public class EnemyComponent : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public async void Die()
        {
            _animator.SetBool(IsDead, true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
            Destroy(gameObject);
        }
    }
}