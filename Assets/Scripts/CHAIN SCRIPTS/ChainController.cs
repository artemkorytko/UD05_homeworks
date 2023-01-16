using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;


public class ChainController : MonoBehaviour
    {
        // висит на всей сцене CHAIN
        [SerializeField] private Button _startbutton;

        [SerializeField] private GameObject _fish;
        private Animator _fishAnimator; // находи кодом!
        [SerializeField] private SpriteRenderer _staticfish;
        [SerializeField] private SpriteRenderer _animatedfish;

        [SerializeField] private GameObject _brevno;

        [SerializeField] private GameObject _ball;
        [SerializeField] private GameObject _vedro;
        [SerializeField] private GameObject _rope;

        [SerializeField] private GameObject _balloon;
        private Animator _balloonanimator;

        [SerializeField] private GameObject _button;
        [SerializeField] private GameObject _pushka;
        

        // скорость падения одной доминошины
        [HideInInspector] public float oneDominoFallsSpeed = 0.2f; 
        
        
        // отдельными скриптами доминошки, с мечтой сделать там наследование
        // туда передаются события
        public event Action GoDomino1;
        // public event Action <UniTask> GoDomino1; //<----- от АК но я так не напишу сама
        public event Action GoDomino2;

        public event Action GetBack;

        //---- события для звуков---------
        public event Action FishFalls;
        public event Action Squish;
        public event Action Pop;
        public event Action Bums;
        public event Action BallinVedro;
        public event Action PinkButtonPressed;
        
        //---- позиции объектов для возврата на место -------
        private Vector3 _fishstartpos;
        private Vector3 _vedrostartpos;
        private Vector3 _balloonpos;
        private Vector3 _ballstartpos;

        // флаг шоб не жали, пока идет анимация:
        private bool chainInProgress = false;

        private void Awake()
        {
            // останавливаем анимацию
            _fishAnimator = _fish.GetComponentInChildren<Animator>();
            _fishAnimator.enabled = false;

            _balloonanimator = _balloon.GetComponent<Animator>();
            _balloonanimator.enabled = false;

            // прячем рыбу без анимации, запоминаем ее начальную позицию
            _staticfish.enabled = false;
            _fishstartpos = _fish.transform.position;
            // ротейшен не берется

            _vedrostartpos = _vedro.transform.position;
            //_startbutton = GetComponent<Button>();

            _ballstartpos = _ball.transform.position;

        }


        private async void Start()
        {  
            // ????????????????????????????????????????????????????????
            // ??????? что надо было написать на рыбу
            // чтобы из нее сделать кнопку,
            // если в компоненте button не появляются функции из этого файла
            // ????????????????????????????????????????????????????????
            //_startbutton.onClick.AddListener(ChainMotion);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (chainInProgress == false)
                {
                    ChainMotion();
                }
            }
        }
        
        

        //------------ монстр-функция про всё по порядку-----------
        private async void ChainMotion()
        {
            chainInProgress = true;
            
            // просто ждем - юзер концентрирует внимание
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            
            
            // рыба падает до бревна
            FishFalls?.Invoke(); // звук заранее переадается в аудоменеджер, где откладывается корутиной
            // тут можно завести переменную для аудиофайла...
            await _fish.transform.DOMove(_fish.transform.position + -_fish.transform.up * 1.65f, 0.5f);
            
            // анимация плюхания рыбы
            _fishAnimator.enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            
            // бревно наклоняется, рыба лежит на нём
            await DOTween.Sequence()
                .AppendCallback(SquishInvokeFunction)
                .Append(_fish.transform.DOMove(_fish.transform.position + -_fish.transform.up * 1.9f, 0.6f))
                .Join(_brevno.transform.DORotate(new Vector3(0, 0, 17), 0.4f))
                ;
            
            // не получилось сделать статичную анимацию *facepalm* и к ней обратьиться, поэтому тасуем чайлдов рыбы
            _animatedfish.enabled = false;
            _staticfish.enabled = true;
            PinkButtonPressed?.Invoke();
            
            GoDomino1?.Invoke();
            // у нас три секунды пока падает домино
            
            // рыба независимо съезжает в пушку,
            _fish.transform.DOMove(_fish.transform.position + -_fish.transform.forward * 0.2f, 1f);
            _fish.transform.DOMove(_fish.transform.position + -_fish.transform.up * 4.2f, 1f).SetEase(Ease.InQuad);
            
            // ждём пока там падает домино
            // было бы здорово в его файле написать геттеры, но я в них пока не понимаю ничего
            await UniTask.Delay(TimeSpan.FromSeconds(oneDominoFallsSpeed * 4)); // 4 шт домино
            
            
            BallinVedro?.Invoke(); //звук
            // шарик катится к краю
            await DOTween.Sequence()
                    .Append( _ball.transform.DORotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360))
                    .Join(_ball.transform.DOMove(_ball.transform.position + _ball.transform.right * 1.7f, 0.5f));
            
            //-------------------------- нижний уровень --------------------------------------------------------
            // шарик падает
            
            await _ball.transform.DOMove(_ball.transform.position - _ball.transform.up * 2.4f, 1).SetEase(Ease.InQuad);
            
            // опускается ведро и шарик в нем
            float lowerrowY = _balloon.transform.position.y + 1.3f; // до координат шарика
            
            Squish?.Invoke();
            await DOTween.Sequence()
                //.AppendCallback(SquishInvokeFunction)
                .Append(_vedro.transform.DOMoveY(lowerrowY, 0.6f).SetEase(Ease.OutBounce))
                .Join(_ball.transform.DOMoveY(lowerrowY, 0.6f).SetEase(Ease.OutBounce))
                ;
            
            // воздушный шар взрывается
            _balloonanimator.enabled = true;
            Pop?.Invoke();
            
            // домино 2 получает событие и запускает юнитаск
            GoDomino2?.Invoke();
            
            // ждём анимацию шарика
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            
            // ведро опускается ниже после взрыва воздушного шарика
            lowerrowY = _balloon.transform.position.y + 0.5f; // до координат шарика
           
            await DOTween.Sequence()
                .Append( _vedro.transform.DOMoveY(lowerrowY, 0.4f))
                .Join(_ball.transform.DOMoveY(lowerrowY, 0.4f));
            
            
            // ждём пока домино там усебя попадают
            await UniTask.Delay(TimeSpan.FromSeconds(oneDominoFallsSpeed * 8)); // 8 шт домино
            
            // нажимается кнопка 
            PinkButtonPressed?.Invoke();
            await _button.transform.DOMove(_button.transform.position + -_button.transform.up * 0.4f, 0.2f);

            // пушка и рыба в ней смещается вниз стреляет рыбой вверх
            await DOTween.Sequence()
                .Append(_pushka.transform.DOMove(_pushka.transform.position + -_pushka.transform.up * 0.4f, 0.2f))
                .Join(_fish.transform.DOMove(_fish.transform.position + -_fish.transform.up * 0.4f, 0.2f));
            Bums?.Invoke(); //звук взрыва в аудои

            // рыба стреляется обратно наверх, пушка в исходное // УДАЛОСЬ на 360!
            await DOTween.Sequence()
                .Append(_fish.transform.DOMove(_fishstartpos, 1f).SetEase(Ease.OutCubic))
                .Join(_pushka.transform.DOMove(_pushka.transform.position + _pushka.transform.up * 0.4f, 0.2f))
                .Join(_fish.transform.DORotate(new Vector3(0, 0, 360f), 1f,  RotateMode.FastBeyond360));

            await GetEverythingBack();
        }

        private async UniTask GetEverythingBack()
        {
            // отжимается кнопка 
            await _button.transform.DOMove(_button.transform.position + _button.transform.up * 0.4f, 0.2f);

            GetBack?.Invoke();
            
            float vedrobackpos = _vedrostartpos.y; // до координат шарика
           
            await DOTween.Sequence()
                .Append( _vedro.transform.DOMoveY(vedrobackpos, 1f))
                .Join(_ball.transform.DOMoveY(vedrobackpos, 1f));

            await _brevno.transform.DORotate(new Vector3(0, 0, -17), 0.4f);

            await _ball.transform.DOMove(_ballstartpos, 0.2f);

            // !!!!!!!!! ТУТ МОЖНО ПРОСТО ПЕРЕЗАПУСТИТЬ АНИМАЦИЮ !!!!!!!!!!!!!
            _balloonanimator.Rebind();
            _balloonanimator.enabled = false;

            // возврат флага, по которому разришаем перезапускать анимацию
            chainInProgress = false;

        }

        // отдельные для звуков чтобы одновременно с анимашкой
        private void SquishInvokeFunction()
        {
            Squish?.Invoke();
        }
    }
