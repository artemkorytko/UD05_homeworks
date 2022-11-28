using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayController : MonoBehaviour
{
    private Text displayText;
    public calculate calculate;

    void Start()
    {
        displayText = GameObject.Find("DispalayText").GetComponent<Text>();
    }

    public void ClearDisplay()
    {
        displayText.text = "";
    }

    public void UpdateDisplayText(string newText)
    {
        displayText.text += newText;
    }

    public void DisplayAnewer()
    {
     string answer = global::calculate(displayText.text);
     displayText.text = answer;
    }
    

}
