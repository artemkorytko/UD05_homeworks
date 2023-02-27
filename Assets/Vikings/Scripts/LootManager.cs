using System.Collections.Generic;
using UnityEngine;

namespace Vikings.Scripts
{
    public class LootManager : MonoBehaviour
    {
        private List<string> _possibleLoot = new List<string>()
        {
            "Рыбу", "Носок", "Меч", "Золото", "Сокровища", "Щит", "Флаг", "Мусор", "Смысл жизни", "Кошку", "Алкоголь"
        };

        public string Find()
        {
            var r = Random.Range(0, _possibleLoot.Count);
            var loot = _possibleLoot[r];
            _possibleLoot.RemoveAt(r);
            return loot;
        }
    }
}