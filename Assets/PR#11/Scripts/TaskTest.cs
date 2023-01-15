using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts
{
    public class TaskTest : MonoBehaviour
    {
        private async void Start()
        {
            StartCoroutine(DoSomeWithDelayCor()); // безопастно использовать + возможность в любой момент остановить - не может ничего не возвращать 
            var a = await DoSomeWithDelay(); // async остановить соложно.. + покрывать нужно try catch(т.к будет вызываться ошибка OperationCanceledException).. + то что может что-то возвращать Debug.Log(a);
            Debug.Log("end Start");
            
        }

        private async Task<int> DoSomeWithDelay() // Task не рекомендованно использовать UniTask
        {
            await Task.Delay(TimeSpan.FromSeconds(5)); // ожидаем 5 сек
            Debug.Log("DoSomeWithDelay");
            return 100;
        }

        // private async Task<string> ReturnStr()
        // {
        //     string str = name;
        //     return str;
        // }

        private IEnumerator DoSomeWithDelayCor()
        {
            yield return new WaitForSeconds(5);
            Debug.Log("DoSomeWithDelayCor");
            var i = 0;
            while (true)
            {
                i ++;
                if(i > 1000)
                    StopCoroutine(DoSomeWithDelayCor());
                
                yield return null;
            }
        }
    }
}