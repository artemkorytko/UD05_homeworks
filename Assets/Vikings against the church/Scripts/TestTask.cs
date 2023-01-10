using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Vikings_against_the_church.Scripts
{
    public class TestTask : MonoBehaviour
    {
        private void Start()
        {
            var outer = Task.Factory.StartNew(() =>     // внешняя задача (Factory - свойство (get)), StartNew метод 
            {
                Debug.Log("Outer task starting...");
                var inner = Task.Factory.StartNew(() =>  // вложенная задача
                {
                    Debug.Log("Inner task starting...");
                    Thread.Sleep(2000);
                    Debug.Log("Inner task finished.");
                }, TaskCreationOptions.AttachedToParent); // TaskCreationOptions - энам AttachedToParent=4, используется в конструкторе
            });
            
            outer.Wait(); // Ждет когда будет выполнена вложенная задача(без TaskCreationOptions.AttachedToParent она отработает)
            Debug.Log("Outer task finished");
        }
        
        

    }
}