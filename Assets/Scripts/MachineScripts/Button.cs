using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private FloorFall _floorFall;
    private BallDestroy _ballDestroy;

    private void Awake()
    {
        _floorFall = FindObjectOfType<FloorFall>();
        _ballDestroy = FindObjectOfType<BallDestroy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(_floorFall.gameObject);
        Destroy(_ballDestroy.gameObject);
    }
}
