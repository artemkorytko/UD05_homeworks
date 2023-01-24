using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Vikings_against_the_church.Scripts
{
    public class ShipSpawnVikings : MonoBehaviour
    {
        [SerializeField] private List<VikingController> vikings;
        
        public List<VikingController> GenereteVikings(int count)
        {
            List<VikingController> vikingControllers = new List<VikingController>(count);

            for (int i = 0; i < count; i++)
            {
                var randomViking = Random.Range(0, this.vikings.Count);
                var viking = Instantiate(this.vikings[randomViking].gameObject,transform.position, Quaternion.identity).GetComponent<VikingController>();
                vikingControllers.Add(viking);
            }
            return vikingControllers;
        }
    }
}