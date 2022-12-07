using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI InputText;
    private double _result;
    private string _input1;
    private string _input2;
    private string _operator;
    private string _del;
    private bool _prov;

    public void ClickNumber(string value)//числа
    {
        if (!_prov)
        {
            _input1 += value;
            InputText.text = _input1;
        }
        else
        {
            _input2 += value;
            InputText.text = _input2;
        }
        
    }
    public void ClickDelete(string del)//delete
    {
        InputText.text = "";
        _input1 = null;
        _input2 = null;
        _operator = null;
        _prov = false;
    }
    
    public void ClickOperator(string oper)//оператор
    {
        _operator = oper;
        _prov = true;
    }

    public void ClickEqual(string val)//решение
    {
        if (_input1!=null && _input2!=null && _operator!= null)
        {
            double a = Double.Parse(_input1);
            double b = double.Parse(_input2);

            switch (_operator)
            {
                case "+":
                    _result = a + b;
                    break;
                case "-":
                    _result = a - b;
                    break;
                case "*":
                    _result = a * b;
                    break;
                case "/":
                    _result = a / b;
                    break;
            }
            InputText.SetText($"{_result}"); //вывод решения
        }
        else
        {
            InputText.SetText($"ошибка, введите все данные"); //вывод решения
        }
        
    }
    
}




