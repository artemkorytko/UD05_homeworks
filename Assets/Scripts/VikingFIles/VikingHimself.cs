using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingHimself : MonoBehaviour
{
    [SerializeField] private GameObject mygold;
    [SerializeField] private GameObject iamviking;

    private V_PriestController _v_priescofile;
    private VikingsController vikifile;

    public bool doIhaveGoldAlready = false;
    
    private void Awake()
    {
        _v_priescofile = FindObjectOfType<V_PriestController>();
        _v_priescofile.BumPoBashke += Ischez; // прихоит из священника, по триггеру в канделябре

        vikifile = FindObjectOfType<VikingsController>();
        vikifile.GotGold += ShowGold;
    }

    void Start()
    {
        // золото в руках викинга пока не видно
        mygold.SetActive(false);
    }

    //============== временная функция пропажи викинга =======================
    private void Ischez(VikingHimself bashka) 
    {
        if (bashka == this)
        {
            // а пусть станет прозрачный - нееее потом задоблбаюсь 
            // iamviking.GetComponent<Renderer>().material.color = new UnityEngine.Color(1, 1, 1, 0.5f);
            iamviking.SetActive(false);
        }
    }
    

    private void ShowGold(VikingHimself oneviking)
    {
        if (oneviking == this)
        {
            // отобразить золото в руках викинга
            mygold.SetActive(true);
            
            // переменная нужна для того, чтобы золото не появлялось, если его убили раньше, чем он взял его
            // пробую к ней подключиться?????????????????
            doIhaveGoldAlready = true;

            // взять ордер от викинга и дать такой же золоту +1
            int vikOrder = oneviking.GetComponent<SpriteRenderer>().sortingOrder;
            mygold.GetComponent<SpriteRenderer>().sortingOrder = vikOrder + 1;
        }
    }
    
    
    private void OnDestroy() // оно надо тут?
    {
        _v_priescofile.BumPoBashke -= Ischez;
        vikifile.GotGold -= ShowGold;
    }
}