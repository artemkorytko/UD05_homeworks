using System;
using TMPro;
using UnityEngine;

namespace Vikings.Scripts
{
    public class NameDisplay : MonoBehaviour
    {
        private Camera cam;

        private void Awake()
        {
            cam = FindObjectOfType<Camera>();
        }

        private void Update()
        {
            var camPos = cam.transform;
            var forward = (transform.position - camPos.position).normalized;
            var up = Vector3.Cross(forward, camPos.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }

        public void SetName(string name)
        {
            var text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = name;
        }
    }
}