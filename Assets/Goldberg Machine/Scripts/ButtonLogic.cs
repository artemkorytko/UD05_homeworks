using System;
using Goldberg_Machine.Scripts;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public event Action ButtonHit;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<TriggerObject>())
        {
            ButtonHit?.Invoke();
        }
    }
}
