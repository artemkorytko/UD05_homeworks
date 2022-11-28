using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class ControllerCulc : MonoBehaviour
    {
        public TextMeshProUGUI result;
        private int x, y;
        private char Prosto;


        public void AddSign(ButtonData data)
        {
            result.text += data.Sign;
        }

        public void Clear()
        {
            result.text = "";
        }

        public void Plus()
        {
            Prosto = '+';
            x = int.Parse(result.text);
            Clear();

        }

        public void Result()
        {

            y = int.Parse(result.text);
            switch (Prosto)
            {
                case '+':
                    result.text = (x + y).ToString();
                    break;


            }


        }

        public void Result1()
        {

            y = int.Parse(result.text);
            switch (Prosto)
            {
                case '/':
                    result.text = (x / y).ToString();
                    break;


            }
        }
    }
}