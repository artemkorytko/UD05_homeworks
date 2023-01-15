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
        
        private void Awake()
        {
            _chainControllerFile = FindObjectOfType<ChainController>();
            //_chainControllerFile.GoDomino1 += Dominos1FallOrder;
        }
        
        private void Dominos1Fall()
        {
            //Dominos1FallOrder();
        }
        
        private async void Start()
        {
            await Dominos1FallOrder();
        }
        
        private async UniTask Dominos1FallOrder()
        {
            await _domino1.transform.DORotate(new Vector3(0, 0, -60), 0.5f);
            await _domino2.transform.DORotate(new Vector3(0, 0, -60), 0.5f);
            await _domino3.transform.DORotate(new Vector3(0, 0, -60), 0.5f);
            await _domino4.transform.DORotate(new Vector3(0, 0, -60), 0.5f);
        }
        
        // Update is called once per frame
        void OnDestroy()
        {
            //_chainControllerFile.GoDomino1 -= Dominos1Fall;
        }
    }
