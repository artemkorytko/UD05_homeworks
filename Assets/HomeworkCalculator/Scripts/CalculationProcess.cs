using System;
using UnityEngine;


public class CalculationProcess : MonoBehaviour
{

    public double Result(double x, double y, char sing)
    {
        switch (sing)
        {
            case '+':
                x += y;
                break;

            case '-':
                x -= y;
                break;

            case '*':
                 x *= y;
                break;

            case '/':
                if (y != 0)
                    x /= y;
                else
                    return 0;
                break;

            case '%':
                x %= y;
                break;
        }

        return x;
    }

}
