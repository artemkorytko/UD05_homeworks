using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    [SerializeField] private GameObject[] _PointForEachVikingForStart;
    
    private Vector3 _PointFin; //конечная точка в которую чел придет
    private string RewardName;
    
    [SerializeField] private float speed = 5f;
     
    


    // Start is called before the first frame update
    void Start()
    {
       // _reward = FindObjectsOfType<Reward>();

        /*  варианты чтобы сделать transform.Translate();
        Vector3.MoveTowards() */
    }

    // Update is called once per frame
    private void Update()
    {

        // transform.Translate(_PointFin.transform.position * speed * Time.deltaTime);
        transform.position =
            Vector3.MoveTowards(transform.position, _PointFin,
                speed * Time.deltaTime); /*MoveTowards - движение от какой-
            либо начальной точки до конечной, начальная позиция - transform.position(та в кот сейчас плеер(а трансформ - это его компонент) и находится;
            _PointFin - является конечной точкой те в кот придем,  speed*Time.deltaTime - макс дистианция)
            */

    }

    private void OnTriggerEnter(Collider other) //на объекте висит коллайдер на котором включен триггер => можем проверить на соударение 
    {
        if (other.TryGetComponent(out Church _church)) //получаем компонет (те проверяем на соударение)
        {

            _PointFin = _church.GetiingOfTargetPointByEachViking(); //
            
        }
        
        if(other.gameObject.TryGetComponent(out Reward reward)) //если удалось получить коллайдер который висит на Reward
        {
            
            RewardName = reward.name; //
            reward.gameObject.SetActive(false); // если зашли в иф => награда выключается 
        }
    
    }

    public void ChangingOfFinPoint(Vector3 point) /*публичный метод который принимает поинт 
    (те можем обратиться к этому методу из другого класса) и менять его значение*/
    {
        _PointFin = point; //в переменную _PointFin присвоили point, а этот point сможем менять из других классов потому что тип метода public
    }
    
}

