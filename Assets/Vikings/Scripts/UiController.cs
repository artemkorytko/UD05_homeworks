using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Vikings.Scripts
{
    public class UiController : MonoBehaviour
    {
        private TextMeshProUGUI _showText;
        private GameManager _gm;

        private void Awake()
        {
            _gm = FindObjectOfType<GameManager>();
            _showText = GetComponentInChildren<TextMeshProUGUI>();
            _showText.text = "Рейд успешен\n \nДобыча викингов:";
        }

        public async void ShowStats(float delay)
        {
            foreach (var viking in _gm.VikingsLoot)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
                _showText.text += $"\n{viking.Key} добыл {viking.Value}";
            }
        }
    }
}