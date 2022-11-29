using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private string value;

    [SerializeField] private DisplayController displayController;
    
    
    public void AppendValueToDisplay()
    { 
        displayController.UpdateDisplayText(value);
    }

    public void EvaluateEquation()
    {
        displayController.DisplayAnswer();
    }

    public void Clear()
    {
        displayController.ClearDisplay();
    }

}
