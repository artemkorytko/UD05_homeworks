using UnityEngine;

namespace Vikings_against_the_church.Scripts
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int _reward;

        public int Reward => _reward;
    }
}