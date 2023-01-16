using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
 using UnityEngine;
 using Cysharp.Threading.Tasks;
 using DG.Tweening;



    public class Dominos1 : MonoBehaviour
    {
        [SerializeField] private GameObject _domino1;
        [SerializeField] private GameObject _domino2;
        [SerializeField] private GameObject _domino3;
        [SerializeField] private GameObject _domino4;
        
        private ChainController _chainControllerFile;
        private float _dominoSpeed;

        private float _dominorotation = -60f;

        public event Action Pum1; //передаем в аудоименеджер
        private void Awake()
        {
            _chainControllerFile = FindObjectOfType<ChainController>();
            _chainControllerFile.GoDomino1 += Dominos1FallOrder;
            _chainControllerFile.GetBack += GetDomino1Back;

            _dominoSpeed = _chainControllerFile.oneDominoFallsSpeed;
            
        }
        
        
        private async void Dominos1FallOrder()
        {
            await _domino1.transform.DORotate(new Vector3(0, 0, _dominorotation), _dominoSpeed);
            Pum1?.Invoke();
            await _domino2.transform.DORotate(new Vector3(0, 0, _dominorotation), _dominoSpeed);
            Pum1?.Invoke();
            await _domino3.transform.DORotate(new Vector3(0, 0, _dominorotation), _dominoSpeed);
            Pum1?.Invoke();
            await _domino4.transform.DORotate(new Vector3(0, 0, _dominorotation), _dominoSpeed);
            Pum1?.Invoke();
        }


        private async void GetDomino1Back()
        {
            await _domino1.transform.DORotate(Vector3.zero, _dominoSpeed);
            await _domino2.transform.DORotate(Vector3.zero, _dominoSpeed);
            await _domino3.transform.DORotate(Vector3.zero, _dominoSpeed);
            await _domino4.transform.DORotate(Vector3.zero, _dominoSpeed);
        }

        private async void Start()
                { 
                    
                }
        
        void OnDestroy()
        {
            _chainControllerFile.GoDomino1 -= Dominos1FallOrder;
            _chainControllerFile.GetBack += GetDomino1Back;
        }
    }
