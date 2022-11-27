using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayController : MonoBehaviour
{
    public Text displayText;
    public Calculator calculator;

    void Start()
    {
        displayText = GameObject.Find("DisplayText").GetComponent<Text>();
    }

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
        string answer = calculator.Calculate(displayText.text);
        displayText.text = answer;
    }
}