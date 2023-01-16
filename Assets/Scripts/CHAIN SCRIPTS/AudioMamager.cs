using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioMamager : MonoBehaviour
{
    [SerializeField] private AudioClip _fishplums;
    [SerializeField] private AudioClip _dominopum;
    [SerializeField] private AudioClip _balloonpop;
    [SerializeField] private AudioClip _squish;
    [SerializeField] private AudioClip _bummm;
    [SerializeField] private AudioClip _ballinvedro;
    [SerializeField] private AudioClip _pinkbutton;
    
    private AudioSource _AudSo;

    private ChainController _chainCofile;
    private Dominos1 _domino1file;
    private Dominos2 _domino2file;
        // Start is called before the first frame update

        private void Awake()
        {
            _AudSo= GetComponent<AudioSource>();
            
            _chainCofile = FindObjectOfType<ChainController>();
            _chainCofile.FishFalls += FishPlumsSound;
            _chainCofile.Squish += SquishSound;
            _chainCofile.Pop += BalloonPopSound;
            _chainCofile.Bums += BumsSound; // пушка
            _chainCofile.BallinVedro += BallinVedroSound;
            _chainCofile.PinkButtonPressed += PinkButtonSound;
            
            _domino1file = FindObjectOfType<Dominos1>();
            _domino1file.Pum1 += DominoPumSound;
            
            _domino2file = FindObjectOfType<Dominos2>();
            _domino2file.Pum2 += DominoPumSound;

        }


        private void FishPlumsSound()
        {
            //_AudSo.PlayOneShot(_fishplums);
            StartCoroutine(FishSoundDelay());
            
            IEnumerator FishSoundDelay()
            {
                
                yield return new WaitForSeconds(0.2f); //это чуть меньше длины анимации рыбы
                _AudSo.PlayOneShot(_fishplums);
            }
        }

        
        private void DominoPumSound()
        {
            _AudSo.pitch = Random.Range(0.9f, 1.1f);
            _AudSo.PlayOneShot(_dominopum);
        }
        
        private void BalloonPopSound()
        {
            _AudSo.PlayOneShot(_balloonpop);
            // образцовая корутина, которая откладывает звук 
            // StartCoroutine(PlayDelayerPop());
            //
            // IEnumerator PlayDelayerPop()
            // {
            //     yield return new WaitForSeconds(0.4f);
            //     _AudSo.PlayOneShot(_balloonpop);
            // }
        }
        
        private void SquishSound()
        {
            _AudSo.PlayOneShot(_squish);
        }
        
        private void BumsSound()
        {
            _AudSo.PlayOneShot(_bummm);
        }

        private void BallinVedroSound()
        {
            // образцовая корутина, которая откладывает звук 
            StartCoroutine(BallVedroDelay());
            
            IEnumerator BallVedroDelay()
            {
                yield return new WaitForSeconds(1f);
                _AudSo.PlayOneShot(_ballinvedro);
            }
        }

        private void PinkButtonSound()
        {
            _AudSo.PlayOneShot(_pinkbutton);
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
}
