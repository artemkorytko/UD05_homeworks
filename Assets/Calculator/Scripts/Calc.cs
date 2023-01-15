using System;
using TMPro;
using UnityEngine;

public class Calc : MonoBehaviour
{
    private float _x;
    private float _y;
    private char _sign;
    private float _result;
    private string _tempText;
    private bool _dotEntered;
    private bool _signEntered;
    private bool _signNotFirst;
    private bool _equalEntered;

    [SerializeField] private TextMeshProUGUI ViewText;

    private void Start()
    {
        _tempText = String.Empty;
        ViewText.text = String.Empty;
        _dotEntered = false;
        _signEntered = false;
        _signNotFirst = false;
        _equalEntered = false;
    }

    public void GetInt(int value)
    {
        if (_tempText == String.Empty && value == 0)
        {
            _tempText += "0,";
            ViewText.text += "0,";
        }
        else
        {
            _tempText += value.ToString();
            ViewText.text += value.ToString();
        }
        _signEntered = false;
        _signNotFirst = true;
        _equalEntered = false;
    }

    public void GetSign(string value)
    {
        if (_signNotFirst)
        {
            if (_signEntered == false)
            {
                if (_x == 0)
                {
                    _x = float.Parse(_tempText);
                }
                else
                {
                    if (_tempText != String.Empty)
                    {
                        _y = float.Parse(_tempText);
                        _x = DoCalc(_x, _y, _sign);
                        ViewText.text += $"\n={_x}";
                    }
                }

                if (_equalEntered)
                {
                    ViewText.text += value;
                    _dotEntered = false;
                    _signEntered = true;
                }
                else
                {
                    ViewText.text += $"\n{value}";
                    _dotEntered = false;
                    _signEntered = true;
                }
            }
            else
            {
                ViewText.text = ViewText.text.Remove(ViewText.text.Length - 1);
                ViewText.text += $"{value}";
                _dotEntered = false;
            }
            
            _sign = Char.Parse(value);
        }
        
        _tempText = String.Empty;
    }

    public void GetDot()
    {
        if (_dotEntered != true && _tempText != String.Empty)
        {
            _tempText += ',';
            ViewText.text += ',';
            _dotEntered = true;
        }
    }

    public void DoEqual()
    {
        if (_tempText != String.Empty && _equalEntered == false)
        {
            _y = float.Parse(_tempText);
            _x = DoCalc(_x, _y, _sign);
            ViewText.text += $"\n={_x}\n";
            _tempText = String.Empty;
            _signEntered = false;
            _dotEntered = false;
            _equalEntered = true;
        }
    }

    public void DoReset()
    {
        ViewText.text = String.Empty;
        _tempText = String.Empty;
        _x = 0;
        _y = 0;
        _result = 0;
        _dotEntered = false;
        _signEntered = false;
        _signNotFirst = false;
    }

    private float DoCalc(float x, float y, char sign)
    {
        switch (sign)
        {
            case '+':
                _result = x + y;
                break;
            case '-':
                _result = x - y;
                break;
            case '*':
                _result = x * y;
                break;
            case '/':
                if (y != 0)
                {
                    _result = x / y;
                }
                else
                {
                    ViewText.text += "\nНа ноль делить нельзя!";
                    _dotEntered = false;
                    _signEntered = false;
                    _signNotFirst = false;
                }
                break;
            case '%':
                _result = x * y / 100;
                break;
            case '^':
                _result = (float)Math.Pow(x, y);
                break;
        }

        return _result;
    }
}