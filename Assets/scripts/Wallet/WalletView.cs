using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _money2;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TextMeshProUGUI _salary;
    [SerializeField] private TextMeshProUGUI _salary2;

    private void Awake()
    {
        _money.text = _wallet.GetMoney().ToString();
        _money2.text = _wallet.GetMoney().ToString();
        _salary.text = _wallet.GetSalary().ToString();
        _salary2.text = _wallet.GetSalary().ToString();
    }

    private void Start()
    {
        _wallet.OnMoneyChanged += value =>
        {
            _money.text = _wallet.GetMoney().ToString();
            _money2.text = _wallet.GetMoney().ToString();
        };

        _wallet.OnSalaryChanged += value =>
        {
            _salary.text = _wallet.GetSalary().ToString();
            _salary2.text = _wallet.GetSalary().ToString();
        };
    }
}
