using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public event Action<Collider, bool> OnTriggerEntered;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEntered?.Invoke(other, true);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerEntered?.Invoke(other, false);
    }
}
