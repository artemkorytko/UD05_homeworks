using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDraw : MonoBehaviour
{
    private LineRenderer _lineRenderer;
   

    // подключить точки, между которыми линия
    [SerializeField] private GameObject _startpoint;
    [SerializeField] private GameObject _endpoint;
    
    // вектора для позиций точек
    private Vector3 _startpointposition;
    private Vector3 _endpointposition;
    
        void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _startpointposition = _startpoint.transform.position;
        
        // эта штука есть в инспекторе но допустим напишем тут
        _lineRenderer.useWorldSpace = true;
    }

    void Start()
    {
        // индекс - это номер точки, между которыми линия: 0,1
        _lineRenderer.SetPosition(0, _startpointposition);
        
        //_dist = Vector3.Distance(_startpointposition, _endpoint.transform.position);
    }

    
    void Update()
    {
        // вычичляем и обновляем конечную точку линии
        _endpointposition = _endpoint.transform.position;
        _lineRenderer.SetPosition(1, _endpointposition);
        

       //????? взяла из урока, боги при чем тут список??????????????
          // List<Vector3> pos = new List<Vector3>();
          // _lineRenderer.SetPositions(pos.ToArray());
          // _lineRenderer.useWorldSpace = true;
        
    }
}
