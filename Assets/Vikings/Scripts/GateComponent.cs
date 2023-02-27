using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vikings.Scripts
{
    public class GateComponent : MonoBehaviour
    {
        private List<GameObject> _barrels = new List<GameObject>();
        public Stack<Vector3> lootPoints = new Stack<Vector3>();

        private void Awake()
        {
            foreach (var barrel in FindObjectsOfType<BarrelComponent>())
            {
                _barrels.Add(barrel.gameObject);
            }

            var sortLootPoints = _barrels.OrderBy(lootPoint =>
                (lootPoint.transform.position - gameObject.transform.position).sqrMagnitude).ToArray();

            foreach (var lootPoint in sortLootPoints)
            {
                lootPoints.Push(lootPoint.transform.position);
            }
        }
    }
}