using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalculatorView : MonoBehaviour
{
    [SerializeField] private TMP_Text _viewData;
    [SerializeField] private TMP_Text _viewResult;
    
    private string _x;
    private string _y;
    private char _sing = ' ';
    private bool _dataX;
    private CalculationProcess _process = new CalculationProcess();
    
    public void InputNumberButtons(string data) // ввод чисел
    {
        if (!_dataX)
        {
            _x += data;
            _viewData.text = _x;
        }
        else
        {
            _y += data;
            _viewData.text = _x + _sing + _y;
        }
    }

    public void InputActionButtons(string data) // ввод символа
    {
        if (_x != null)
        {
            _sing = Char.Parse(data);
            _viewData.text = _x + _sing;
            _dataX = true;
        }
    }

    public void Equals(string sing) // вывод результата
    {
        if (sing == "=")
        {
            double result = _process.Result(Double.Parse(_x), Double.Parse(_y), _sing);
        
            _viewData.text = _x + _sing + _y + "=";
            _viewResult.text = result.ToString();
        
            _x = result.ToString();
            _y = null;
        }
        else if(_y != null)
        {
            _x = _process.Result(Double.Parse(_x), Double.Parse(_y), _sing).ToString();
            _viewData.text = _x + sing;
            _viewResult.text = _x;
            _y = null;
        }

    }

    public void ResetData() // сброс данных
    {
        _x = null;
        _y = null;
        _sing = ' ';
        _viewData.text = null;
        _viewResult.text = null;
        _dataX = false;
    }
}
