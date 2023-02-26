using System.Collections.Generic;
using UnityEngine;

namespace Vikings.Scripts
{
    public class VikingNamer : MonoBehaviour
    {
        private List<string> Names = new List<string>()
            { "Хроддгейр", "Бродди", "Эгиль", "Стюр", "Ульф", "Бейнир", "Гуннар", "Орм", "Бьорн", "Ингвар", "Торгельд", "Эйвинд", "Олаф", "Эрик", "Борис" };

        public string NameViking()
        {
            var x = Random.Range(0, Names.Count);
            var name = Names[x];
            Names.RemoveAt(x);
            return name;
        }
    }
}