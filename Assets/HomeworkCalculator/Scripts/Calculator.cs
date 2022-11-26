using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Calculator : MonoBehaviour
{
    
   [SerializeField] private TMP_Text _inputData;
   [SerializeField] private TMP_Text _resultText;


   public void ViewResult()
   {
      ChekInputData(_inputData.text);
      _resultText.text = _inputData.text;
   }

   private void ChekInputData(string inputData)
   {
      
   }

}
