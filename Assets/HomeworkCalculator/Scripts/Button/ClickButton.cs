using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _viewData;
    [SerializeField] private TMP_Text _viewResult;
    
    private string _x;
    private string _y;
    private char _sing = ' ';
    private bool _dataX = false;
    
    public void InputNumberButtons(string data)
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

    public void InputActionButtons(string data)
    {
        _sing = Char.Parse(data);
        _dataX = true;
        _viewData.text = _x + _sing;
    }

    public void Equals()
    {
        double result = Double.Parse(_x) + Double.Parse(_y);
        _viewData.text = _x + _sing + _y + "=";
        _viewResult.text = result.ToString();
        _x = result.ToString();
    }

    public void ResetData()
    {
        _x = null;
        _y = null;
        _dataX = false;
        _sing = ' ';
        _viewData.text = null;
        _viewResult = null;
    }
}
