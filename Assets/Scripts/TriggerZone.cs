using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    //public event Action<Collider> ColliderEntering; 
    public event Action<bool> AlarmStateChanged;

    private void OnTriggerEnter(Collider other)
    {
        //ColliderEntering?.Invoke(other);
        if (other.gameObject.GetComponent<Thief>())
            AlarmStateChanged?.Invoke(true);
    }

    private void OnTriggerExit(Collider other)
    {
        //ColliderEntering?.Invoke(other);
        if (other.gameObject.GetComponent<Thief>())
            AlarmStateChanged?.Invoke(false);
    }
}
