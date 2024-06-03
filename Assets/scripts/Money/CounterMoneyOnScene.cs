using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterMoneyOnScene : MonoBehaviour
{
    public static CounterMoneyOnScene Instance { get; private set; }

    private int _volue = 0;

    public event Action OnChangeVolue;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddVolue(int money)
    {
        _volue += money;

        OnChangeVolue?.Invoke();
    }

    public void SubtractVolue(int money)
    {
        _volue -= money;

        OnChangeVolue?.Invoke();
    }

    public int GetVolue()
    {
        return _volue;
    }

    public int GetAllSalary()
    {
        int volue = _volue + Wallet.Instance.GetSalary();

        return volue;
    }

    public void RestartVolue()
    {
        _volue = 0;
    }
}
