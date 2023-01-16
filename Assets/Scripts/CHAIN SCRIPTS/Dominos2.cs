using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;


    public class Dominos2 : MonoBehaviour
    {
        [SerializeField] private GameObject _domino1;
        [SerializeField] private GameObject _domino2;
        [SerializeField] private GameObject _domino3;
        [SerializeField] private GameObject _domino4;
        [SerializeField] private GameObject _domino5;
        [SerializeField] private GameObject _domino6;
        [SerializeField] private GameObject _domino7;
        [SerializeField] private GameObject _domino8;

        private ChainController _chainControllerFile;
        
        private float _dominoSpeed;
        private float _dominorotation;

        public event Action Pum2;

        private void Awake()
        {
            // подключаем главный файл и ждем оттуда событие
            _chainControllerFile = FindObjectOfType<ChainController>();
            _chainControllerFile.GoDomino2 += Dominos2FallOrder;
            _chainControllerFile.GetBack += GetDomino2Back;
            
            // скорость берем в главном файле
            _dominoSpeed = _chainControllerFile.oneDominoFallsSpeed;
            //------------ЗАДАЕМ ПОВТОРОТ-----------------
            _dominorotation = 60f;
        }

        
        
        private async void Dominos2FallOrder()
        {
            await _domino1.transform.DORotate(new Vector3(0, 0, _dominorotation), _dominoSpeed);
            Pum2?.Invoke();
            await _domino2.transform.DORotate(new Vector3(0, 0, _dominorotation), _dominoSpeed);
            Pum2?.Invoke();
            await _domino3.transform.DORotate(new Vector3(0, 0, _dominorotation), _dominoSpeed);
            Pum2?.Invoke();
            await _domino4.transform.DORotate(new Vector3(0, 0, _dominorotation), _dominoSpeed);
            Pum2?.Invoke();
            await _domino5.transform.DORotate(new Vector3(0, 0, _dominorotation), _dominoSpeed);
            Pum2?.Invoke();
            await _domino6.transform.DORotate(new Vector3(0, 0, _dominorotation), _dominoSpeed);
            Pum2?.Invoke();
            await _domino7.transform.DORotate(new Vector3(0, 0, _dominorotation), _dominoSpeed);
            Pum2?.Invoke();
            await _domino8.transform.DORotate(new Vector3(0, 0, _dominorotation), _dominoSpeed);
            Pum2?.Invoke();
        }

        private async void GetDomino2Back()
        {
            await _domino1.transform.DORotate(Vector3.zero, _dominoSpeed);
            await _domino2.transform.DORotate(Vector3.zero, _dominoSpeed);
            await _domino3.transform.DORotate(Vector3.zero, _dominoSpeed);
            await _domino4.transform.DORotate(Vector3.zero, _dominoSpeed);
            await _domino5.transform.DORotate(Vector3.zero, _dominoSpeed);
            await _domino6.transform.DORotate(Vector3.zero, _dominoSpeed);
            await _domino7.transform.DORotate(Vector3.zero, _dominoSpeed);
            await _domino8.transform.DORotate(Vector3.zero, _dominoSpeed);
        }
        
        // Update is called once per frame
        void OnDestroy()
        { 
            _chainControllerFile.GoDomino1 -= Dominos2FallOrder;
            _chainControllerFile.GetBack -= GetDomino2Back;
        }
    }

