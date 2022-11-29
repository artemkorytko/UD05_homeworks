using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLogic : MonoBehaviour
{
    [SerializeField] private Text showText;
    [SerializeField] private Text showText1;
    [SerializeField] private string value;
    private string textString;
    private char[] separators = {'+', '-', '*', '/', '^'};
    private string[] splitText;
    private bool enterSign = false;
    private char sign;
    [SerializeField] private double x;
    [SerializeField] private double y;
    
    public void OnClickButton()
    {
        if (value != "+" && value != "-" && value != "*" && value != "/" && value != "^")
        {
            showText.text += value;
            
        }
        else if (enterSign == false)
        {
            sign = Convert.ToChar(value);
            x = Convert.ToDouble(showText.text);
            print(x);
            showText.text += "\n";
            showText.text += value;
            showText.text += "\n";
            enterSign = true;
        }
        else if (enterSign)
        {
            showText.text += "\nСотри всё и введи выражение из двух частей!";
        }
    }
    
    public void ClearAll()
    {
        showText.text = null;
        enterSign = false;
    }
    
    public void Clear()
    {
        if (showText != null)
        {
            textString = showText.text;
            textString = textString.Remove(textString.Length - 1);
            showText.text = textString;
        }
    }

    public void OnClickCalc()
    {
        splitText = showText.text.Split(separators);
        y = Convert.ToDouble(splitText[1]);
        print(x + " " + y + " " + sign);
        Calculation Calc = new Calculation(x, y, sign);
        showText.text = Calc.Calculate();
        enterSign = false;
    }
}
