using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Church : MonoBehaviour
{
    private TargetPoint[] _targetPoint;

    private Stack<TargetPoint> TargetDistance = new Stack<TargetPoint>(10); 
    // Start is called before the first frame update
    private void Start()
    {
        _targetPoint = FindObjectsOfType<TargetPoint>();
        SortingTargetDistance();
    }

    private void SortingTargetDistance()
    {
        var SortingDistance = _targetPoint.OrderBy(p => Vector3.Distance(transform.position, p.transform.position));
        
        foreach (var Point in SortingDistance)
        {
            TargetDistance.Push(Point);
           // Point.ActivationOfRewards();
        }
        
    }

    public Vector3 GetiingOfTargetPointByEachViking() // публичный метод которым сможем пользоваться в PlayerMotion, чтобы Викинги сомгли получить свои TargetPoint от церкви
    {
        return TargetDistance.Pop().transform.position;//возвращаем (берем) из стека каждый элемент(обратно в стек не возаращем) и возвращаем его положение
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
