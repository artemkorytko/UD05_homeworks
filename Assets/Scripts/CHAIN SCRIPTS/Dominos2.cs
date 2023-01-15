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

        private void Awake()
        {
            _chainControllerFile = FindObjectOfType<ChainController>();
            //_chainControllerFile.GoDomino2 += Dominos2Fall;
        }

        private void Dominos2Fall()
        {
            Dominos2FallOrder();
        }

        private async void Start()
        {
            await Dominos2FallOrder();
        }

        private async UniTask Dominos2FallOrder()
        {
            await _domino1.transform.DORotate(new Vector3(0, 0, 60), 0.2f);
            await _domino2.transform.DORotate(new Vector3(0, 0, 60), 0.2f);
            await _domino3.transform.DORotate(new Vector3(0, 0, 60), 0.2f);
            await _domino4.transform.DORotate(new Vector3(0, 0, 60), 0.2f);
            await _domino5.transform.DORotate(new Vector3(0, 0, 60), 0.2f);
            await _domino6.transform.DORotate(new Vector3(0, 0, 60), 0.2f);
            await _domino7.transform.DORotate(new Vector3(0, 0, 60), 0.2f);
            await _domino8.transform.DORotate(new Vector3(0, 0, 60), 0.2f);
        }

        // Update is called once per frame
        void OnDestroy()
        {
           // _chainControllerFile.GoDomino1 -= Dominos1Fall;
        }
    }

