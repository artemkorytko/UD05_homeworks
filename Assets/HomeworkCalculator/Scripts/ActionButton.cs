using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ActionButton : MonoBehaviour
{ 
    [SerializeField] private string _action;
    
    public string Action => _action;
}
