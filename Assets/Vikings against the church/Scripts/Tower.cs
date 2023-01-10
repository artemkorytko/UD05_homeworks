using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vikings_against_the_church.Scripts
{
    public class Tower : MonoBehaviour
    {
        private Queue<Coin> _queueCoins = new Queue<Coin>(10);
        private Coin[] _coins;
        
        private void Awake()
       {
           _coins = GetComponentsInChildren<Coin>();
       }

        private void Start()
        {
            SortArray();
            FillQueue();
        }

        private void SortArray()
        {
            var orderByDescending = _coins.OrderByDescending(coin => Vector3.Distance(transform.position, coin.transform.position)).ToArray();
            _coins = orderByDescending;
        }

        private void FillQueue()
        {
            for (int i = 0; i <_coins.Length; i++)
                _queueCoins.Enqueue(_coins[i]);
        }

        public Coin GetTrargetCoin()
        {
            return _queueCoins.Dequeue();
        }
    }
}