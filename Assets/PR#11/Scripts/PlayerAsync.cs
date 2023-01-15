using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class PlayerAsync : MonoBehaviour
    {
        // async могут быть только те юнити методы которые отрабатывают по одному разу(?)

        [SerializeField] private Transform ship;
        [SerializeField] private Transform capsule;
        [SerializeField] private Transform startPoint;
       
        private CancellationToken _cancellationToken;
        
        private async void Start()
        {

            _cancellationToken = this.GetCancellationTokenOnDestroy(); // остановить дотвин в методе MoveShip (WithCancellation(_cancellationToken))
            
            await MoveShip(startPoint.position);
            capsule.position = startPoint.position + startPoint.forward * 0.5f;
            
            await MoveCapsule();
            capsule.gameObject.SetActive(false);
            
            await MoveShip(startPoint.position - startPoint.forward * 3f);
            await UniTask.Yield(); // подождать кадр 
            await UniTask.WaitForFixedUpdate(); // подождать отработку физики
           // await UniTask.WaitWhile(() => delay == 1);
            Debug.Log("Log start");
            
            await UniTask.Delay(TimeSpan.FromSeconds(5), cancellationToken: _cancellationToken); // остановка Юнитаски 
            
            Debug.Log("Log end");
        }

        private async UniTask<int> myTask()
        {
            await UniTask.Yield();
            return 100;
        }

       
        private async UniTask MoveShip(Vector3 vector3)
        {
            await ship.DOMove(vector3, 2).WithCancellation(_cancellationToken); // установили токен для остановки юнитаски
        }

        private async UniTask MoveCapsule()
        {
            await capsule.DOMove(capsule.position + capsule.forward * 5f, 2);
            await capsule.DOJump(capsule.position, 2, 1, 1).Join(capsule.DORotate(new Vector3(0,180,0), 1 )); 
            await capsule.DOMove(capsule.position + capsule.forward * 5f, 2);
        }
    }
}