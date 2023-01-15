using UnityEngine;

namespace Vikings_against_the_church.Scripts
{
    public class CoinPoint : MonoBehaviour
    {
        [SerializeField] private int _reward;
        
        public int Reward => _reward;
    }
}