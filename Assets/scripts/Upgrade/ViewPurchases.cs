using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewPurchases : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textNumberAttemptsBuySpeed;
    //[SerializeField] private TextMeshProUGUI _textNumberAttemptsBuyChair;
    [SerializeField] private TextMeshProUGUI _textNumberAttemptsBuyMateriale;
    [SerializeField] private TextMeshProUGUI _textNumberAttemptsBuyMoney;

    [SerializeField] private Upgrade _upgrade;

    private void Start()
    {
        _upgrade.OnBuySpeedPlayer += () =>
        {
            _textNumberAttemptsBuySpeed.text = _upgrade.CountPaySpeed.ToString();
        };

        //_upgrade.OnBuyChairInventory += () =>
        //{
        //    _textNumberAttemptsBuyChair.text = _upgrade.CountPayChair.ToString();
        //};

        _upgrade.OnBuyMoney += () =>
        {
            _textNumberAttemptsBuyMoney.text = _upgrade.CountPayMoney.ToString();
        };

        _upgrade.OnBuyDeskInventory += () =>
        {
            _textNumberAttemptsBuyMateriale.text = _upgrade.CountPayDesk.ToString();
        };
    }
}
