using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public static Balance Instance { get; private set; }

    private int _balance = 0;

    public event Action<int> OnBalanceChanged;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public int GetMoney()
    {
        return _balance;
    }

    public void TakeMoney(int money)
    {
        _balance += money;

        OnBalanceChanged?.Invoke(money);
    }

    public void StartNewLevel()
    {
        _balance = 0;

        OnBalanceChanged?.Invoke(0);
    }

    //смена языка leanLocalization
    //при потере фокуса ставиться на паузу 
    //при рекламе тоже ставиться на паузу
}

