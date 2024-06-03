using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalaceView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _money2;
    [SerializeField] private Balance _balance;

    private void Awake()
    {
        _balance.OnBalanceChanged += value =>
        {
            _money.text = _balance.GetMoney().ToString();
            _money2.text = _balance.GetMoney().ToString();
        };

        _money.text = _balance.GetMoney().ToString();
        _money2.text = _balance.GetMoney().ToString();
    }
}
