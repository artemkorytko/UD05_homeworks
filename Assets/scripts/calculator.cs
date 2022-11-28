using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class calculate : MonoBehaviour
{
   public string Calculate(int x, int y, char sing)
   {
      string result = null;
      switch (sing)
      {
         case '+':
            result = DoSum(x, y);
            break;

         case '-':
            result = Dominus(x, y);
            break;

         case '/':
            result = DoDevision(x, y);
            break;
         case '*':
            result = DoMulti(x, y);
            break;
      }

      return result;
   }
   
   private string DoMulti(int x, int y)
      {
         if (y == 0)
         {
            return null;
         }

         return (x * y).ToString();
      }

      private string DoDevision(int x, int y)
      {
         return (x / y).ToString();
      }
      private string Dominus(int x, int y)
      {
         return (x - y).ToString();
      }
      private string DoSum(int x, int y)
      {
         return (x + y).ToString();
      }

   
}
