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

        [Tooltip("Скорость изменения громкости сигнализации.")]
        [SerializeField][Range(0.01f, 1f)] private float _changeVolumeSpeed = 0.25f;

        private Coroutine _alarmCoroutine;

        private float _minVolumeAlarm = 0f;
        private float _maxVolumeAlarm = 1f;

        private void Start()
        {
            _alarmSound.volume = _minVolumeAlarm;
        }

        private void Stop()
        {
            if (_alarmCoroutine != null)
            {
                StopCoroutine(_alarmCoroutine);
                _alarmCoroutine = null;
            }
        }

        public void Work(bool isInside)
        {
            Stop();
            bool isGrow;

            if (isInside)
            {
                isGrow = true;
                _alarmCoroutine = StartCoroutine(FadeVolume(_maxVolumeAlarm, isGrow));
            }
            else if (isInside == false)
            {
                isGrow = false;
                _alarmCoroutine = StartCoroutine(FadeVolume(_minVolumeAlarm, isGrow));
            }
        }

        private IEnumerator FadeVolume(float target, bool isGrow)
        {
            if (isGrow)
            {
                _alarmSound.Play();
                while (_alarmSound.volume < target)
                {
                    _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, target, _changeVolumeSpeed * Time.deltaTime);

                    yield return null;
                }
            }
            else if (isGrow == false)
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
}