using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Vikings_against_the_church.Scripts
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float jumpPowerCoin = 5f;
        
        private CoinPoint[] _coinsPoint;
        private Queue<CoinPoint> _queueCoins = new Queue<CoinPoint>(10);

        private Coin[] _coinses;
        private Stack<Coin> _coinsesStack = new Stack<Coin>(10);
        private void Awake()
       {
           _coinsPoint = GetComponentsInChildren<CoinPoint>();
           _coinses = GetComponentsInChildren<Coin>();
       }
        
        private void Start()
        {
            SortArray();
            FillingCollections();
        }

        private void FillingCollections()
        {
            foreach (var coinView in _coinses)
                _coinsesStack.Push(coinView);
            
            for (int i = 0; i <_coinsPoint.Length; i++)
                _queueCoins.Enqueue(_coinsPoint[i]);
        }

        private void SortArray()
        {
            var orderByDescending = _coinsPoint.OrderByDescending(coin => Vector3.Distance(transform.position, coin.transform.position)).ToArray();
            _coinsPoint = orderByDescending;
        }
        

        public async UniTask<Transform> GetTrargetCoin()
        {
            var coinPoint = _queueCoins.Dequeue().transform;
            var coinView = _coinsesStack.Pop().transform;

            await coinView.DOJump(coinPoint.position + new Vector3(0,0.5f,0), jumpPowerCoin, 1, 2).SetEase(Ease.InOutSine)
                .Join(coinView.DORotate(new Vector3(90,360,0), 2, RotateMode.FastBeyond360));
           
            
            return coinPoint;
        }
        
    }
}