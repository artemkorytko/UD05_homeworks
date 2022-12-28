using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Vikings_against_the_church.Scripts
{
    public class SpawnViking : MonoBehaviour
    {
        [SerializeField] private List<Viking> _vikings;
        
        public Stack<Viking> GenereteVikings(int count)
        {
            Stack<Viking> vikings = new Stack<Viking>(count);

            for (int i = 0; i < count; i++)
            {
                var randomViking = Random.Range(0, _vikings.Count);
                var viking = Instantiate(_vikings[randomViking].gameObject, transform).GetComponent<Viking>();
                vikings.Push(viking);
            }

            return vikings;
        }
        
    }
}