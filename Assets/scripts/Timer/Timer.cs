using System;
using System.Collections;
using UnityEngine;

namespace GameTime
{
    public class Timer : MonoBehaviour
    {
        private const float _delyeCoroutine = 1f;
        private WaitForSeconds _timeCoroutine = new WaitForSeconds(_delyeCoroutine);
        private int _timeLeft;
        private IEnumerator _tickCoroutine;

        public event Action OnStarted;
        public event Action<int> OnTick;
        public event Action OnDone;

        public void StartTimer(int seconds)
        {
            _timeLeft = seconds;

            if (_tickCoroutine != null)
            {
                Stop();
            }

            _tickCoroutine = TickCoroutine();
            StartCoroutine(_tickCoroutine);
            OnStarted?.Invoke();
        }

        public void Stop()
        {
            StopCoroutine(_tickCoroutine);
        }

        public float GetTimeLife()
        {
            return _timeLeft;
        }

        private IEnumerator TickCoroutine()
        {
            while (_timeLeft >= 1)
            {
                _timeLeft--;
                OnTick?.Invoke(_timeLeft);
                yield return _timeCoroutine;
            }

            OnDone?.Invoke();
        }

        public void FinishShift()
        {
            _timeLeft = 1;
        }
    }
}