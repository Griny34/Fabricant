using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _moneyGrowth;
    [SerializeField] private int _elapsedTime;

    private int _money = 0;
    private float _time = 0;

    public event Action<int> OnMoneyChanged;

    private void Start()
    {
        MatchModel.Instace.OnFinished += () =>
        {
            _money = 0;
            _time = 0;
            OnMoneyChanged?.Invoke(_money);
        };
    }

    private void Update()
    {
        GrowthMoney();
    }

    public int GetMoneyEnemy()
    {
        return _money;
    }

    private void GrowthMoney() 
    {
        _time += Time.deltaTime;

        if(_time >= _elapsedTime)
        {
            _time = 0;

            _money += _moneyGrowth;

            OnMoneyChanged?.Invoke(_moneyGrowth);
        }
    }
}
