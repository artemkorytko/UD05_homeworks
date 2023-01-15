using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Scripts.PR
{
    public class Phase1 : MonoBehaviour
    {
        [SerializeField] private Transform bullet;
        [SerializeField] private Transform robber;
        [SerializeField] private Transform target;

        private Sequence _mySequence;
        private async void Start()
        {
            _mySequence = DOTween.Sequence();
            await Shoot();
        }

        private async UniTask Shoot()
        {
            await _mySequence.Append(robber.DORotate(new Vector3(0, 0, 50), 2f))
                .Join(bullet.DOMove(target.position, 2f));
            
            await _mySequence.Append(robber.DORotate(new Vector3(0, 0, 0), 2));
        }
        
    }
}