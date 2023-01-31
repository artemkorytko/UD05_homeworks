using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Resources;


public class Motionnnn : MonoBehaviour
{

    
    [SerializeField] private Transform Sphere;
    [SerializeField] private GameObject[] Brick;
    //[SerializeField] private Transform Capsule;
    private async void Start()
    {
        await Sphere.DOMove(Brick[0].transform.position, 3);
        //await Sphere.DOJump;
    }
    
   /* private void OnTriggerEnter(Collider other) //OnTRiggerEnter - т к финиш является триггером(триггер потому что не коллижн, коллижн когда ударились об него и оттолкнулись, иначе - триггер) (т е мы коснулись нашего триггера - финиша)
    {
        if (other.gameObject.GetComponent<Capsule>()) /*FinishComponent - jтдельный класс который повесим на
            объект чтобы он мог существовать и этот параметр можно было применять в логине//проверка на соударение(коллизию) /
             / вернется тру или фолс: тру когда получилось получить компонент, фолс - когда не получилось получить компонент 
             WallComponent//отправляем Wallcomponent - компонент который висит на стене => 
            
           // если  GetComponent - возвращает тру или фолс 
         {
             Capsule.DOJump(Capsule.position, 5, 10, 5 ); // если коснулись триггера (то на что повесим FinishComponent) т е получили компонент воллкомпонент след-но
             Sequence sequence = DOTween.Sequence();
             sequence.Append(Capsule.DOMove(Capsule.position + new Vector3(2, 0.5f, 0.2f), 3)).Join(Capsule.DORotate(new Vector3(0,1440,0), 3));
         }
 
  */
        
}       
            // если  GetComponent - возвращает тру или фолс 
 

    

