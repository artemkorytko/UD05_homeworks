using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Numerics;
using DG.Tweening;
using Runner;
using TMPro;
//using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using Color = System.Drawing.Color;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
//using Sequence = Unity.VisualScripting.Sequence;
using Vector3 = UnityEngine.Vector3;

//================ КОНТРОЛЛЕР ВСЕЙ СЦЕНЫ ТУТ ============================================

public class VikingsController : MonoBehaviour
{
    [Serializable] public class Viking
    {
        public bool GoldShown;
        public int Health;
        public Vector3 V_position;
        public float Doordist;

        public GameObject VikingObject;

        // блок типо для инкапсуляции? :/
        public Viking(bool goldShown, int health, Vector3 vPosition, float doordist,
            GameObject vikingObject)
        {
            GoldShown = goldShown;
            Health = health;
            V_position = vPosition;
            Doordist = doordist;
            VikingObject = vikingObject;
        }
    }


    public class Gold
    {
        public Vector3 G_position;
        public GameObject GoldObject;
        public Gold(Vector3 gPosition, GameObject goldObject) // че пишууууу?
        {
            G_position = gPosition;
            GoldObject = goldObject;
        }
    }

    //------------------ ПЕРЕМЕННЫЕ И ПРЕФАБЫ ----------------------------------------------------

    // [SerializeField] private GameObject _priest;
    [SerializeField] private GameObject _goldprefab;
    [SerializeField] private GameObject _vikingprefab;
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _outpoint;

    [SerializeField] private float _padding = 0.5f; // расстановка по сетке

    public List<Viking> _vikingsArray; // инит массива для викингов
    public List<Gold> _goldArray; 
    
    private VikingKorabl _vikingKorablfile; // подключение файла с кораблем
    [SerializeField]  int howmanyvikings = 13; // 
    
    private VikingsController _thisfile; // для события внутри ЭТОГО ФАЙЛА
    private V_PriestController _v_priescofile;

    // все для правильной расстановки слоев
    private int ord_no;
    private int gold_layerorder = 50;
    private int viking_layerorder = 100;
    
    // private bool AllowVikingsGo; // разрешаем идти к цервки (старая анимация)
    
    // первый вошел - священник поднимет канделябр
    bool flagFirstEntered = false; 

    private event Action VikingiVyshli;
    public event Action <VikingHimself> GotGold;
    public event Action FirstVikingEntered;

    public Vector3 doorpos;
    private Vector3 outpos;
    private float outposOffset = 0.2f; // сдвиг викингов на финальной позиции возле корабля в рядочек

    private Queue<Viking> _vikQueue;
    private Stack<Viking> _vikStack;

    private DG.Tweening.Sequence _vikToGoldAnimation;
    
    //---------------------------------------------------------------------------------------------

    
    //---------------- массив цветов для покраски викингов (шоб разные были) ----------------------- 
    private static List<string> _colorlist = new List<string>()
        { //"green", "white", "blue", "pink", "yelow", "cyan", "magenta", "black" - это никак  не всовывалось в .Color
          // thisViking.GetComponent<Renderer>().material.color = UnityEngine.Color.red;
          // даже через всякие KnownColor и преобразования...
          // ?????????????????????????????????????????????????????????????????????????????????????
          // ???????????? а нет ли способа преобразовать string в команду ????????????????????????
          // ?????????????????????????????????????????????????????????????????????????????????????
          
            "220,20,60", "255,20,147","139,0,0","173,255,47","106,90,205","139,0,139", "255,127,80"
        };
    private int howmanycolors = _colorlist.Count; // length - ленгтх само меняет на каунт


    //-----------------------------------------------------------------------------------------------
    private void Awake()
    {
        _vikingKorablfile = FindObjectOfType<VikingKorabl>();
        _vikingKorablfile.Priplyl += VikingiIzKorablya; //подписка на приплытие изнутри корабля

        _thisfile = FindObjectOfType<VikingsController>();
        _v_priescofile = FindObjectOfType<V_PriestController>();
    }


    //-----------------------------------------------------------------------------------------------
    void Start()
    {
        LayGold();

        // по концу цикла расстановки викингов должно сработать событие ВикингиВышли
        _thisfile.VikingiVyshli += VikingiToCHurch;
        
        // когда викинг получает по башке - анимация киляется и инстантиэйтим новое золото на месте происшествия
        _v_priescofile.BumPoBashke += DropGold;
    }

    
    //---------------------- разложить золото в церкви ---------------------------------------------------------------
    
    #region LayGold

    private void LayGold()
    {
        // у золота свой массив
        _goldArray = new List<Gold>(howmanyvikings);

        int ord_no_gold = 0; // для слоев 
        var _gstartX = -1.2f;
        var _gstartY = 1f;
        int RowLength = 7;

        //--------- разложить золото ------------------------------------------------------------------------------
        for (int i = 0; i < howmanyvikings; i++)
        {
            _gstartX = _gstartX + _padding; //типо сдвинет вправо

            if (i == RowLength) // переход на вторую строчку
            {
                _gstartY = _gstartY - _padding * 0.8f; //как есть
                _gstartX = -1.2f + _padding;
            }

            Vector3 g1Pos = new Vector3(_gstartX, _gstartY, 0);
            // ----  свежерассчитанная позиция 
           
            
            var thisGold = Instantiate(_goldprefab, g1Pos, Quaternion.identity, transform);
            
            // сначала ставит, потом фигачит в массив с позицией и ссылкой (!) на конкретное золотишко
            _goldArray.Add(new Gold(g1Pos, thisGold));
             //_goldarray[i].GoldObject = thisGold;
             //_goldarray[i].G_position = g1Pos;
             
            ord_no_gold++;
            // ord_no надо, чтобы они правильно накладывались, gold_layerorder задан в начале
            thisGold.GetComponent<SpriteRenderer>().sortingOrder = gold_layerorder + ord_no;
        }
    }

    #endregion
    
    //---------------------- викинги выходят -------------------------------------------------------------------------
    private void VikingiIzKorablya()
    {
        // основной массив с винингами
        _vikingsArray = new List<Viking>(howmanyvikings);

        //------- стартовая позиция первого викинга
        var _vstartX = -4.5f;
        var _vstartY = -1.5f;

        int RowLength = 4;
        
        string thisvikingcolorname = null; // для рандомного цвета 

        //=========== цикл делает викингов, сует в массив и расставляет в стартовую позицию ==========================
        for (int i = 0; i < howmanyvikings; i++)
        {
            
            //------------ расстановка рядочками (можно наверное изящней, но как есть)-------------------
            _vstartX = _vstartX + _padding; // сдвинет вправо

            if (i == RowLength) // переход на вторую строчку
            {
                _vstartY = _vstartY - _padding * 2; 
                _vstartX = -4f;
            }

            if (i == RowLength * 2) 
            {
                _vstartY = _vstartY - _padding * 2.5f; 
                _vstartX = -4f;
            }

            //------------- заполняет массив -------------------------------------
            Vector3 v1Pos = new Vector3(_vstartX, _vstartY, 0);
            _vikingsArray.Add(new Viking(false, // пока без золота
                
                100, // пока все здоровенькие - на случай health_bar у каждого... 
                v1Pos, // а почему тут пполе не подписывается?
                0,
                _vikingprefab
            ));

            // это нормально вообще, корутина в цикле? Иначе тупо откладывает все сразу :/
            int secs = i;

            StartCoroutine(WaitaSec());

            IEnumerator WaitaSec()
            {
                float waittime = secs * 0.2f; // задержка между появлением викингов
                yield return new WaitForSeconds(waittime);

                //---------- штампуем викингов -------------------------------
                var thisViking = Instantiate(_vikingprefab, _vikingsArray[secs].V_position, Quaternion.identity,
                    transform);

                //// !!!
                _vikingsArray[secs].VikingObject = thisViking;
                _vikingsArray[secs].V_position = thisViking.GetComponent<SpriteRenderer>().transform.position;
                //// !!!

                // Order! ------!!! номер в слое !!!--------------------------
                ord_no++;
                thisViking.GetComponent<SpriteRenderer>().sortingOrder = viking_layerorder + ord_no;

                //--------- красим двоих из каждых трёх ------------------
                int thisvikingcolor = Random.Range(0, howmanycolors);
                thisvikingcolorname = _colorlist[thisvikingcolor];
                
                var paintornottopaint = Random.Range(0, 3);
                if (paintornottopaint == 0 || paintornottopaint == 1 )
                {
                    string[] zzzz = thisvikingcolorname.Split(",");
                    int rr = Int32.Parse(zzzz[0]);
                    int gg = Int32.Parse(zzzz[1]);
                    int bb = Int32.Parse(zzzz[2]);

                    float rf = rr / (float) 255;
                    float gf = gg / (float) 255;
                    float bf = bb / (float) 255;
                    
                    thisViking.GetComponent<Renderer>().material.color = new UnityEngine.Color(rf, gf, bf, 1);
                }
                
                
                
                // Debug.Log(" тадам "); // ==> ОНО ОТКЛАДЫВАЕТ ЭТО ДЕЙСТВИЕ НА 3с !!!!!!!!!!!!

                // гениальная мысль от мужа!!!!
                if (secs == howmanyvikings - 1)
                    VikingiVyshli?.Invoke();
            }
        } // конец цикла for
        
    } //конец викинги из корабля
    //=================================================================================================================

    
    
    
    void VikingiToCHurch()
    {
        float vdoordist = 0; // переменная для высчитывания

        //get position of the door -- Vector3 
        doorpos = _door.transform.position;
        
        // берет с объекта котороый поставили руками координату, куда викинги будут выходить
        outpos = _outpoint.transform.position;

        // item ибо у нас класс
        foreach (Viking item in _vikingsArray)
        {
            // !!!!
            Vector3 xx = item.V_position;

            // каждому викингу высчитываем дистанцию и пихаем в его проперти !!!!
            vdoordist = Vector3.Distance(xx, doorpos);

            // суём дистанцию в массив
            item.Doordist = vdoordist;

            //Debug.Log($"Dist for Viking {_vikingsArray.IndexOf(item)} -- {vdoordist}");
        }

        // пересортировать список викингов в зависимости от ближайшего к двери
        _vikingsArray = _vikingsArray.OrderBy(ch => ch.Doordist).ToList();

        // теперь по очереди должны идти к двери - в Update! // AllowVikingsGo = true;

        // пихание викингов в ОЧЕРЕДЬ ------------------------------------------------------------------ :/
        _vikQueue = new Queue<Viking>(howmanyvikings);
        _vikStack = new Stack<Viking>(howmanyvikings);

        
        for (int i = 0; i < howmanyvikings; i++)
        {
            _vikQueue.Enqueue(_vikingsArray[i]);
        }
        
        // у нас есть очередь викингов, первым стоит крайний к двери
        for (int i = 0; i < howmanyvikings; i++)
        {
            var whichGoldPile = _goldArray[i];
            
            GameObject queueviking = _vikQueue.Peek().VikingObject;
            
            _vikToGoldAnimation = DOTween.Sequence()
                .AppendInterval(i)
                .Append(queueviking.transform.DOMove(doorpos, 2))
                .AppendInterval(0.3f)
                .AppendCallback(PriestReady)
                .Append(queueviking.transform.DOMove(whichGoldPile.G_position, 0.5f))
                .AppendCallback(IhavetheGold)
                .AppendInterval(15f)
                .AppendCallback(NowGoAway);

            // у чела в очереди берет его компоненту и вызывает изменения в его личном экземпляре префаба (показал золото)
            void IhavetheGold()
            {
                // кучке золота с этим номером надо отключить видимость
                whichGoldPile.GoldObject.SetActive(false);
                if (queueviking.TryGetComponent(out VikingHimself oneviking))
                {
                    // это чудно работает
                    GotGold?.Invoke(oneviking);
                    
                    // ?????????????????????????????????????????????????????????????????????????????
                    // вот чего я не понимаю - это как обратиться к этому полю(свойству?) из проверки
                    // "кого треснули по башке" - в файле PriestColntroller,
                    // если там речь про физический объект на сцене, а не про изначальный массив..или?
                    // ????????????????????????????????????????????????????????????????????????????????
                    _vikingsArray[i].GoldShown = true;
                }
            }
            
            // выкинет из очереди 
            _vikQueue.Dequeue();
            
            // потом засунуть его жев стак............
            _vikStack.Push(_vikingsArray[i]);   
             
             // Debug.Log($"Stack {_vikStack.Count} and -- Queue{_vikQueue.Count}" ); 
        } // конец for
        
    } // конец викинги ту черч

    
    
    //--------------- первый вошел - передает в священника и меняет флаг -----------------------------------------------
    void PriestReady()
    {
        if (!flagFirstEntered) // проверка, чтобы больше функция не запускалось
        {
            // событие что викинги ломятся - уходит священнику и он поднимает канделябр
            FirstVikingEntered?.Invoke();
            Debug.Log("Первый Викинг вошел!!!");
            flagFirstEntered = true;
        }
    }

    
    //============================= вторая часть анимации когда золото взяли ===========================================
    void NowGoAway()
    {
        // читает крайнего в стаке и запускает ему анимацию выхода из церкви
        GameObject stackviking = _vikStack.Peek().VikingObject;
            CarryGoldOut();
            // выкидывает из стака
            _vikStack.Pop();
            
            //--------------- анимация выхода из церкви и выноса золота --------------
            // ???????????????????????????????????????????????????????????????????????????????
            // вот тут у меня где-то глобальная проблемка - ЕСЛИ время ожидания между анимациями настроить так, 
            // что первый взял золото и вышел, ПОКА последний не зашел - 
            // все нафиг перепутывается и несколько викингов остаются тусить в церкви
            // ???????????????????????????????????????????????????????????????????????????????
            void CarryGoldOut()
            { 
                _vikToGoldAnimation = DOTween.Sequence()
                .Append(stackviking.transform.DOMove(doorpos, 1))
                .AppendInterval(0.3f)
                .Append(stackviking.transform.DOMove(new Vector3(outpos.x + outposOffset,outpos.y ,outpos.z), 2));

                outposOffset += 0.2f;
            }
    }

    
    //--------------- роняние золота на месте кончины - по событию бумбашка из священника --------
    void DropGold(VikingHimself bashka)
    {
        // получить позицию убитого викинга
        Vector3 dropGoldPos = bashka.transform.position;
        _vikToGoldAnimation.Kill();
        
        // кидаем туда новое золото
        // ЕСЛИ внутри викинга он его уже взял - прикооол это на весь файл ссылка там в скобочках!!!
        if (bashka.doIhaveGoldAlready == true)
        { var droppedGold = Instantiate(_goldprefab, dropGoldPos, Quaternion.identity, transform);}
    }

    void Update()
        {
            // if (AllowVikingsGo)
            // {
            //     // старая безтвиновая анимация похода викингов от корабля до двери, а затем к золоту
            //     // StartCoroutine(GoToGold());
            // }
        }
    
        #region Old Animation in Update
    
            //----------------------------- идут до двери и до золота ---------------------------------------------------
            // IEnumerator GoToGold()
            // {
            //     foreach (Viking item in _vikingsArray)
            //     {
            //         Vector3 v = item.VikingObject.GetComponent<SpriteRenderer>().transform.position;
            //
            //         if (item.isGoToDoor)
            //         {
            //             v = Vector3.MoveTowards(v, doorpos, 5 * Time.deltaTime);
            //             if (Vector3.Distance(v, doorpos) == 0)
            //             {
            //                 item.isGoToDoor = false;
            //                 item.isGoToGold = true;
            //             }
            //         }
            //
            //         if (item.isGoToGold)
            //         {
            //             v = Vector3.MoveTowards(v, _goldarray[_vikingsArray.IndexOf(item)].G_position, 5 * Time.deltaTime);
            //             if (Vector3.Distance(v, _goldarray[_vikingsArray.IndexOf(item)].G_position) == 0)
            //             {
            //                 item.isGoToDoor = false;
            //                 item.isGoToGold = false;
            //                 item.GoldShown = true;
            //
            //                 if (item.VikingObject.TryGetComponent(out VikingHimself oneviking))
            //                 {
            //                     GotGold?.Invoke(oneviking);
            //                 }
            //             }
            //             
            //             // в очереь всех 
            //         }
            //
            //         // Задать новый ордер для викинга - задние позади, передние впереди
            //         item.VikingObject.GetComponent<SpriteRenderer>().sortingOrder = 150 + (_vikingsArray.IndexOf(item) * 2);
            //         item.VikingObject.GetComponent<SpriteRenderer>().transform.position = v;
            //
            //         yield return new WaitForSeconds(0.3f);
            //     }
        #endregion
    
    
    
        private void OnDestroy()
        {
            _vikingKorablfile.Priplyl -= VikingiIzKorablya;
        }
        
}