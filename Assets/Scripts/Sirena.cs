using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

namespace Assets.Scripts
{
    [RequireComponent(typeof(AudioSource))]

    public class Sirena : MonoBehaviour
    {
        [SerializeField] private AudioSource _alarmSound;
        [SerializeField] private TriggerZone _triggerZone;

        [Tooltip("Скорость изменения громкости сигнализации.")]
        [SerializeField][Range(0.01f, 1f)] private float _changeVolumeSpeed = 0.25f;

        private Coroutine _alarmCoroutine;

        private float _minVolumeAlarm = 0f;
        private float _maxVolumeAlarm = 1f;

        private void Stop()
        {
            if (_alarmCoroutine != null)
            {
                StopCoroutine(_alarmCoroutine);
                _alarmCoroutine = null;
            }
        }

        private void OnEnable()
        {
            _triggerZone.OnTriggerEntered += Work;
        }

        private void OnDisable()
        {
            _triggerZone.OnTriggerEntered -= Work;
        }

        private void Work(Collider collider, bool isInside)
        {
            Stop();

            if (collider.gameObject.GetComponent<Thief>() && isInside)
            {
                _alarmCoroutine = StartCoroutine(FadeInVolume(_maxVolumeAlarm));
            }
            else if (collider.gameObject.GetComponent<Thief>() && isInside == false)
            {
                _alarmCoroutine = StartCoroutine(FadeOutVolume(_minVolumeAlarm));
            }
        }

        private IEnumerator FadeInVolume(float target)
        {
            _alarmSound.volume = _minVolumeAlarm;
            _alarmSound.Play();

            while (_alarmSound.volume < target)
            {
                _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, target, _changeVolumeSpeed * Time.deltaTime);

                yield return null;
            }
        }

        private IEnumerator FadeOutVolume(float target)
        {
            while (_alarmSound.volume > target)
            {
                _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, target, _changeVolumeSpeed * Time.deltaTime);

                yield return null;
            }

            _alarmSound.Stop();
        }
    }
}