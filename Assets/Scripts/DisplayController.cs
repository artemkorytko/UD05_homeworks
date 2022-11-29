using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayController : MonoBehaviour
{
    public Text displayText;
    public Calculator calculator;
    
    
    public void ClearDisplay()
    {
        displayText.text = "";
    }

    public void UpdateDisplayText(string newText)
    {
        displayText.text += newText;
    }

    public void DisplayAnswer()
    {
        if (!String.IsNullOrWhiteSpace(displayText.text))
        {
            string answer = calculator.Calculate(displayText.text).ToString();
            displayText.text = answer;
        }
        else
        {
            displayText.text = "Пустая строка";
        }
    }
}