using System;
using TMPro;
using UnityEngine;

namespace Vikings.Scripts
{
    public class VikingUiController : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void UpdateUi(string vikName, string subject)
        {
            var camPos = FindObjectOfType<Camera>().transform;
            var forward = (transform.position - camPos.position).normalized;
            var up = Vector3.Cross(forward, camPos.right);
            transform.rotation = Quaternion.LookRotation(forward, up);

            _text.text = $"{vikName}\nнашел\n{subject}";
        }
    }
}