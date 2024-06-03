using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private int _timeLeft;
    private IEnumerator _tickCoroutine;

    public event Action OnStarted;
    public event Action<int> OnTick;
    public event Action OnDone;

    public void StartTimer(int seconds)
    {
        _timeLeft = seconds;

        if(_tickCoroutine != null)
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
            yield return new WaitForSeconds(1f);
        }

        OnDone?.Invoke();
    }

    public void FinishShift()
    {
        _timeLeft = 1;
    }
}
