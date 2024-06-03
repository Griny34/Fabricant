using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MatchView : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] private MatchModel _matchModel;

    [Header("UIPlayer")]
    [SerializeField] private TextMeshProUGUI _playerProgress2;

    private void Start()
    {
        Wallet.Instance.OnMoneyChanged += value =>
        {
            _playerProgress2.text = Wallet.Instance.GetMoney().ToString();
        };

        //_enemyFirst.OnMoneyChanged += value =>
        //{
        //    UpdateUI(_enemyFirstProgress.text, _enemyFirst.GetMoneyEnemy());
        //    UpdateUI(_enemyFirstProgress2.text, _enemyFirst.GetMoneyEnemy());
        //};

        //_enemySecond.OnMoneyChanged += value =>
        //{
        //    UpdateUI(_enemySecondProgress.text, _enemySecond.GetMoneyEnemy());
        //    UpdateUI(_enemySecondProgress2.text, _enemySecond.GetMoneyEnemy());
        //};
    }

    private void UpdateUI(string text, int value)
    {
        text = value.ToString();
    }
}
