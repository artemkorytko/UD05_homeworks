using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;



    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _VikingPlayer; //массив из всевозможных типов викингов (те сколько у нас префабов плееров, а не сколько вкингов на сцене(тк префабы(типы)викнгов могут повторяться))

        [SerializeField] private GameObject[] _PointForEachVikingForStart;
        
        

        [SerializeField] private float delay; // объявляем переменную задержки которую будет использовать в корутинах
        
        
        private Church _church;
        private List<PlayerMotion> AllPlayers = new List<PlayerMotion>(10);//создаем лист чтобы засунуть туда всех плееров после инстантиэйта //указываем размерность 10 - чтобы не выделялось куча памяти под лист
        private InitialPoint _initialPoint;
        private Queue<PlayerMotion> VikingsQueueGoingToChurch = new Queue<PlayerMotion>(10);
        private Stack<PlayerMotion> VikingsGoingBackToShip = new Stack<PlayerMotion>(10);
        private Reward[] _reward; //создаем массив эл-ов типа "Награда"



        // private Vector3 StartPosition = new Vector3(0, 0, 9);

        private void Awake()
        {
            //GetCOmponent - это использование компонента данного объекта в инспекторе (напр у объекта GsmeObject - transform(в кот указаны координаты))

            _initialPoint = FindObjectOfType<InitialPoint>();
            _church = FindObjectOfType<Church>();
            _reward = FindObjectsOfType<Reward>(); //находим на сцене все объекты типа Награда

        }
        
        

        private void Start() //на старте отключаем все награды (чтобы челы не пособирали их раньше времни)
        {
            VikingInitializationAndSpawn();

            for (int i = 0; i < _reward.Length; i++) //пребираем все эл-ты из массива Наград
            {
                _reward[i].gameObject.SetActive(false);//на старте отключаем все награды (чтобы челы не пособирали их раньше времни)
            }
           

        }

        
        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))//когда нажата левая кнопка мыши войдем в цикл и обратимся к методу
            {
                DistanceBetweenVikingAndChurch(); // обращение к методу
            }

            if (Input.GetMouseButtonDown(1)) //по нажатию правой кнопки мыши отработает метод двжиения викингов с TargetPoint до корабля обратно
            {
                MovingFromTargetToShip();
            }
        }
        
        private void VikingInitializationAndSpawn()
        {
            for (int i = 0; i < _PointForEachVikingForStart.Length; i++)
            {
                int RandomKoeff = Random.Range(0, _VikingPlayer.Length);
                var VikingInitialization = Instantiate(_VikingPlayer[RandomKoeff].gameObject, _initialPoint.transform.position,
                    Quaternion.Euler(0, 0, 0)).GetComponent<PlayerMotion>();; /*  _initialPoint.transform.position:
                     _initialPoint - это объект, transform - этого компонент данного объекта типа ГеймОБджект(трансформ- компонент который виден в инспекторе(хранит координаты))
    VikingInitialization GetComponent<PlayerMotion> - т е инициализириуем викинга(из префаба) и получаем ему компонент(те вешаем скрипт) PlayerMotion, и как следствие все созданные из префаба будут знать как им двигаться(что и прописано в скрипте)
 */
                VikingInitialization.ChangingOfFinPoint(_PointForEachVikingForStart[i].transform.position); //обратились к методу ChangingOfFinPoint который public в классе ChangingOfFinPoint, а вместо Vector3 point передаем vector _PointForEachVikingForStart[i].transform.position
                AllPlayers.Add(VikingInitialization);//добавили в лист заиниченного викинга
 
                
                
                // _VikingPlayer[i].gameObject.transform.position = new Vector3(_PointStartPosition[i].x,);
            }
        }

        private void DistanceBetweenVikingAndChurch()
        {
            
           // Vector3.Distance(AllPlayers[0].transform.position, _church.transform.position);//вектор3.дистанс - метод, который рассчитывает расстояние между жвумя объектами a и b (т е находит b-a)
            var Sorting = AllPlayers.OrderBy(p => Vector3.Distance(p.transform.position, _church.transform.position));/* переменная которая хранит лист отсортированный по p, где p - каждый
             викинг, а сортировка выполнена по расстоянию от текузего положения викинга до церкви   (+см строчку выше) ((по умолчанию сортировка выполнена от меньшего к наибольшему))*/
            foreach (var Viking in Sorting) //цикл для каждого викинга в созданной выше коллекции "Sorting" //в foreach обращаемся не по индексу а к конкретному элементу (Viking)
            {
                VikingsQueueGoingToChurch.Enqueue(Viking);//засовываем викингов в queue(ее назв-е -> VikingsQueueGoingToChurch)
               // Debug.Log(Viking.name);
               VikingsGoingBackToShip.Push(Viking);//засовываем в стек виикнгов отсортированных по положения викинга от начальной точки до того как дошел до цервкви до самой церкви, и в этом же порядке засовываем их в стек
            }
            
            StartCoroutine(GoingToChurch());
            ActivationOfRewards(); // когда челы пойдут к цервки обратимся к методу "Активация наград"
        }

        private void MovingFromTargetToShip()
        {
            for (int i = 0; i < _PointForEachVikingForStart.Length; i++) //создаем цикл чтобы отработало для каждого викинга на сцене, а викингов столько - сколько точек для начальных позиций
            {
                VikingsGoingBackToShip.Pop()
                    .ChangingOfFinPoint(_initialPoint.transform.position); /* берем из стека эл-т
         (те от последнего к первому), метод ChangingOfFinPoint, и засовываем туда позицию точки до которой викингам надо дойти,
         а конечная точка = начальная точка в которой происходило их инициирование*/
            }
        }

        private IEnumerator GoingToChurch() //создаем корутину чтобы могли создать задержку в начале двжиения между каждым викингом на 1 секунду
        {
            foreach (var Viking in VikingsQueueGoingToChurch) //цикл, для каждого викинга (объявили переменную Viking) из очереди VikingsQueueGoingToChurch
            {
                
                Viking.ChangingOfFinPoint( _church.transform.position);/* тк в PlayerMOtion прописано действие 
                передвижения, мы просто переменную там обозначающую конечную цель (_PointFin) переименовываем
                 в ту, которая сейчас будет у нас являться конечной целью (те положение церкви)*/
                yield return new WaitForSeconds(delay);
            }          
            
        }
        
        
        private void ActivationOfRewards()
        {
            for (int i = 0; i < _reward.Length; i++)
            {
                _reward[i].gameObject.SetActive(true); //активируем все эл-ты из массива Наград
            }
        }




    }
