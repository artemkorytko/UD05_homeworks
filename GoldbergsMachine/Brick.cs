using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Resources;
using UnityEngine;
using DG.Tweening;

public class Brick : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Capsule capsule;
    [SerializeField] private Cylinder cylinder;

    private Vector3 CapsulePos;
    private async void OnTriggerEnter(Collider other) //OnTRiggerEnter - т к финиш является триггером(триггер потому что не коллижн, коллижн когда ударились об него и оттолкнулись, иначе - триггер) (т е мы коснулись нашего триггера - финиша)
    {

        CapsulePos = capsule.transform.position;
            
       // if (other.gameObject.TryGetComponent(out Cylinder cylinder)) /*FinishComponent - jтдельный класс который повесим на
            //объект чтобы он мог существовать и этот параметр можно было применять в логине//проверка на соударение(коллизию) /
             // вернется тру или фолс: тру когда получилось получить компонент, фолс - когда не получилось получить компонент 
             //WallComponent//отправляем Wallcomponent - компонент который висит на стене => 
          //  */
        
            // если  GetComponent - возвращает тру или фолс 
        
            await capsule.transform.DOJump(CapsulePos, 5, 10, 5 ); // если коснулись триггера  
            Sequence sequence = DOTween.Sequence();
            await sequence.Append(capsule.transform.DOMove(new Vector3(11, 1.0f, 0.2f), 3)).Join(capsule.transform.DORotate(new Vector3(0,14400,0), 3));
        

       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
