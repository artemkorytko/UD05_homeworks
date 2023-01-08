using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Calculator : MonoBehaviour
{
    // Start is called before the first frame update

    //[SerializeField] private TMP_InputField inputField;
    public TextMeshProUGUI InputText;
    public double result;
    public double input;
    private char operation;
    public int number1;
    public int number2;
    public string sign;
   

    private string _tempText;

    public void On_Click_button_number(int number)
    {
        InputText.text += number.ToString();
        if (number1 == 0)
        {
            number1 = number;
        }
        else
        {
            number2 = number;
        }

    }
    
    public void On_Click_Operation(char sign1)
    {
        
        InputText.text = sign1.ToString();
        operation = sign1;
        //operation = sign;

    }
    
    public void On_Click_Equal(string eq)
    {
        InputText.text = eq.ToString();
        if (number1 != 0 && number2 != 0 && !string.IsNullOrEmpty(sign)) ;
        switch (operation)
        {
            case '+':
            
                result = number1 + number2;
                break;
            
            case '-':
            
                result = number1 - number2;
                break;
            
            case '*':
            
                result = number1 * number2;
                break;
            
            case '/':
            
                if (number2 != 0)
                {
                    result = number1 / number2;
                }

                else 
                {
                    result = 0;
                }

                break;
            
            
            InputText.SetText(result.ToString());
        }
    }
    
    public void On_Click_period(string dot)
    {
        InputText.text = dot.ToString();
    }

    public void Do_Reset()
    {
        number1 = 0;
        number2 = 0;
       // InputText.IsEmpty;
    }

    public void Calculation()
    {
    }

    private void Start()
    {
        
    }
}
