using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Vikings_against_the_church.Scripts
{
    public class SpawnVikings : MonoBehaviour
    {
        [SerializeField] private List<Viking> _vikings;
        
        public List<Viking> GenereteVikings(int count)
        {
            List<Viking> vikings = new List<Viking>(count);

            for (int i = 0; i < count; i++)
            {
                var randomViking = Random.Range(0, _vikings.Count);
                var viking = Instantiate(_vikings[randomViking].gameObject,transform.position, Quaternion.identity).GetComponent<Viking>();
                vikings.Add(viking);
            }

            return vikings;
        }
        
    }
}