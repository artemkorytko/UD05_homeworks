using System;

namespace DefaultNamespace
{
    public class Calculation
    {
        private double _x;
        private double _y;
        private double _result;
        private char _sign;
        private bool _error;
        private string _answer;
        
        public Calculation(double x, double y, char sign)
        {
            _x = x;
            _y = y;
            _sign = sign;
        }
        
        public string Calculate()
        {
            _error = false;
            if (_sign == '/' && _y == 0)
            {
                _error = true;
            }
            if (_error == false)
            {
                switch (_sign)
                {
                    case '+':
                        _result = _x + _y;
                        break;
                    case '-':
                        _result = _x - _y;
                        break;
                    case '*':
                        _result = _x * _y;
                        break;
                    case '/':
                        _result = _x / _y;
                        break;
                    case '^':
                        _result = (Math.Pow(_x, _y));
                        break;
                }
                
                _answer = Convert.ToString(_result);
            }
            else
            {
                _answer = "На ноль делить нельзя!";
            }

            return _answer;
        }
    }
}