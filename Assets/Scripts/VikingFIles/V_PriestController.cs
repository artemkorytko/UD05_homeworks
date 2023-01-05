using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class V_PriestController : MonoBehaviour
{
    [SerializeField] private GameObject _priest;
    [SerializeField] private GameObject _kandelabr;

    private Rigidbody _priestrigidbody;
    private Rigidbody _kandrigidbody;

    private VikingsController _vikifile;
    
    private float moveSpeed = 5f; // скорость священника
    private bool zamah = false; // флаг для поднятия канделябра - О_о пишет ошибку что не используется???

    private bool isPressedSpace = false; // флаг нажатого пробела

    // по событию BumPoBashke: в главном файле отсанавливается анимация и появляется золото на месте убийства
    // в самом викинге отключается видимость викинга
    public event Action<VikingHimself> BumPoBashke; 

    private void Awake()
    {
        // найти внутренний ригибади у канделябра
        _priestrigidbody = GetComponent<Rigidbody>();
        _kandrigidbody = _kandelabr.GetComponentInChildren<Rigidbody>();

        // подписка на главный файл, где событие что первый зашел
        _vikifile = FindObjectOfType<VikingsController>();
        _vikifile.FirstVikingEntered += PodnjatKandeliabr;
    }

    private void PodnjatKandeliabr()
    {
        _kandelabr.transform.Rotate(0, 0, -90);
        zamah = true;
    }

    // ??????????????????????????????????????????????????????????????????????????????????????????
    // не работает от слова вообще, даже если сувать в апдейт на каждую функцию
    // ???????????????????? как кодом не дать выходить за пределы церкви???????
    // или как-то отдельным слоем ставить невидимые стены ?????????????????????
    // ??????????????????????????????????????????????????????????????????????????????????????????
    private void StayInChurch()
    {
        var position = _priest.transform.position;

        float priestXbounds = _priestrigidbody.position.x;
        priestXbounds = Mathf.Clamp(priestXbounds, -1.1f, 3.77f);

        float priestYbounds = _priestrigidbody.position.y;
        priestYbounds = Mathf.Clamp(priestYbounds, -1.29f, 0.57f);
    }


    private void Update()
    {
        StayInChurch(); // НЕ РАБОТАЕТ

        //----------------- кнопки двигания попа -----------------------------------------
        if (Input.GetKey(KeyCode.UpArrow))
        { _priest.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime); }
        
        if (Input.GetKey(KeyCode.DownArrow))
        { _priest.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime); }

        if (Input.GetKey(KeyCode.LeftArrow))
        { _priest.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); }

        if (Input.GetKey(KeyCode.RightArrow))
        { _priest.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); }

        if (Input.GetKey(KeyCode.Space))
        {
            // выходим, если пробел уже был нажат...
            if (isPressedSpace)
                return;

            // устанавливаем флаг нажатого пробела
            isPressedSpace = true;
            
            BumKandelabrom(); // машет канделябром

            // выходим из обработки пробела
            return;
        }

        // снимаем флаг нажатого пробела
        isPressedSpace = false;
        
    } //end update

    //-------------------------- функция удара канделябром по викингу -----------------------------------
    void BumKandelabrom()
    {
        StartCoroutine(WaitTillPodnimet());

        IEnumerator WaitTillPodnimet()
        {
            yield return StartCoroutine(BUMS());
            zamah = true;
        }

        IEnumerator BUMS()
        {
            // Debug.Log("вжух");
            _kandelabr.transform.Rotate(0, 0, 90);
            
            yield return new WaitForSeconds(0.3f);
            _kandelabr.transform.Rotate(0, 0, -90);
            zamah = false;
        }
    }
    
    
    
    //-------------------- высчитывает, по какому викингу треснули (триггер на конце канделябра)--------------------------
    void OnTriggerEnter(Collider predmet)
    {
        Debug.Log("OnTriggerEnter: Ата-та!");

        if (predmet.transform.TryGetComponent(out VikingHimself bashka))
        {
            GotaViking(bashka);
        }
    }

    //----------------- вызывает пропажу викинга ТОЛЬКО в случае удара -------------------
    private void GotaViking(VikingHimself bashka)
    {
        if (isPressedSpace)
        {
            BumPoBashke?.Invoke(bashka);
        }
    }


    private void OnDestroy()
    {
        _vikifile.FirstVikingEntered -= PodnjatKandeliabr;
    }

}