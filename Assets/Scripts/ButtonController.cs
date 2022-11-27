using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public string value;

    public DisplayController displayController;

    void Start() 
    {
        displayController = GameObject.Find("DisplayPanel").GetComponent<DisplayController>();
    }

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
