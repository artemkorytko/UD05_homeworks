using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Finish : MonoBehaviour
{
    private FinishBall _finishBAll;

    private void Awake()
    {
        _finishBAll = FindObjectOfType<FinishBall>();
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 204, 102);
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }
}
