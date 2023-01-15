using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


    public class ChainController : MonoBehaviour
    {
        // висит на всей сцене CHAIN
        [SerializeField] private Button _startbutton;

        [SerializeField] private GameObject _fish;
        private Animator _fishAnimator; // находи кодом!

        [SerializeField] private GameObject _brevno;

        [SerializeField] private GameObject _ball;
        [SerializeField] private GameObject _vedro;

        [SerializeField] private GameObject _balloon;
        private Animator _balloonanimator;

        [SerializeField] private GameObject _button;
        [SerializeField] private GameObject _pushka;

        // отдельными скриптами доминошки, с мечтой сделать там наследование
        // туда передаются события
         public event Action GoDomino1;
        // public event Action GoDomino2;

        private void Awake()
        {
            // останавливаем анимацию
            _fishAnimator = _fish.GetComponent<Animator>();
            _fishAnimator.enabled = false;

            _balloonanimator = _balloon.GetComponent<Animator>();
            _balloonanimator.enabled = false;


        }


        private async void Start()
        {
            // ? ДОПИСАТЬ ВЕРЕВКУ К ВЕДРУ

            
            // пришлось писать сюда, хотя хотела на кнопку, дабы юзер хоть что-то делал
            await ChainMotion();
        }

        private void Update()
        {
            // тут найти про кнопку и запустить функцию шоб все задвигалось

            //??????????????????????????????????????????????
            // как приделать код юнитаска к кнопке????
            //??????????????????????????????????????????????
            //_startbutton.onClick.AddListener(ChainMotion);
        }


        private async UniTask ChainMotion()
        {
            // просто ждем
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            
            //рыба падает до 
            await _fish.transform.DOMove(_fish.transform.position + -_fish.transform.up * 2f, 0.5f);
            _fishAnimator.enabled = true;

            // бревно наклоняется
            await DOTween.Sequence()
                .Append(_fish.transform.DOMove(_fish.transform.position + -_fish.transform.up * 2f, 1f))
                .Join(_brevno.transform.DORotate(new Vector3(0, 0, 17), 1f));

            GoDomino1?.Invoke();

            // у нас три секунды пока падает домино
            
            // БЛИН да как тупо включть у аниматора другую анимацию???????
            // ?????????????????????
            // или хотя бы кадр один????????????????
            //  _animator.SetFloat("Speed", _joystick.Direction.magnitude); - это из собаки
            // _fishAnimator.CrossFade("Idle");
            // SetAnimation
            _fish.transform.DOMove(_fish.transform.position + -_fish.transform.up * 3f, 1f).SetEase(Ease.InQuad);


        }




    }
