using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public partial class Calculator : MonoBehaviour
{
    private static char[] chars = { '+', '-', '*', '/', '%' };
    public bool minusFlag = false;
    

    public double Calculate(string displayText)
    {
        
        if (displayText[0] == '-')                                                                                      //Проверка на наличие минуса первым символом
        {
            minusFlag = true;                                                                                           //Установка флага умножения на минус и удаление минуса в начале строки
            displayText = displayText.Remove(0, 1);
        }
        return DoAction(displayText);
    }

    private double DoAction(string displayText)
    {
        string[] str;
        char oper;
        double x;
        double y;
        double sum = x = y =0;
        str = displayText.Split(chars, 2); 
        oper = SignSearch(displayText); 
        x = double.Parse(str[0]);
        if (minusFlag)                                                                                                  //домнажение на -1 при наличии минуса первым символом в строке
        {
            x = x * -1;                                                                                                 
        }
        y = double.Parse(str[1]);
        if ((oper == '/' || oper == '*') && (x== 0 || y == 0)) 
        {
            sum = 0;
        } else {
            sum = CalculateLogic(x,oper,y);
        }

        minusFlag = false;                                                                                              //Обноление флага после нажатия равно
        return sum;
}
    
    private char SignSearch(string str)
    {
        int index = -1;
        for (int i = 0; i < chars.Length; i++)
        {
            if (index == -1)
                index = str.IndexOf(chars[i]);
        }

        return str[index];
    }

    public double CalculateLogic(double x,char oper,double y)
    {
        double sum = 0;
        switch (oper)
        {
            case '-':
                sum = x - y;
                break;
            case '*':
                sum = x * y;
                break;
            case '/':
                sum = x / y;
                break;
            case '%':
                sum = x * (y / 100);
                break;
            default:
                sum = x + y;
                break;
            
        }
        
        return sum;
    }

    
}