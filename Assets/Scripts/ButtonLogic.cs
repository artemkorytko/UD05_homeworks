using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ButtonLogic:MonoBehaviour
    {
        public ButtonData Data;
        public Button Button;
        public ControllerCulc Controller;

        private void Start()
        {
            Button.onClick.AddListener((() => Controller.AddSign(Data)));
        }
    }
}