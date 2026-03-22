using Assets.Scripts;
using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] private Sirena _sirena;
    [SerializeField] private TriggerZone _triggerZone1;

    private bool _isThiefInside;

    private void OnEnable()
    {
        _triggerZone1.AlarmStateChanged += OnSirena;
    }

    private void OnDisable()
    {
        _triggerZone1.AlarmStateChanged -= OnSirena;
    }

    private void OnSirena(bool isInside)
    {
        _sirena.Work(isInside);
    }
}
