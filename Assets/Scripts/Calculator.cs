using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCalc;

public class Calculator : MonoBehaviour
{
    public string Calculate(string equation)
    {
        Expression expression = new Expression(equation);
        return expression.Evaluate().ToString();
    }

}