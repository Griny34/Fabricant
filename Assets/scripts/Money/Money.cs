using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public static Money Instance { get; private set; }

    [SerializeField] private int _value;
    [SerializeField] private int _upgradeMoney;

    private int _startMoney = 10;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("money"))
        {
            _value = PlayerPrefs.GetInt("money");
        }
        else
        {
            _value = _startMoney;
        }

        //Upgrade.Instace.OnBuyMoney += () =>
        //{
        //    UpgradeMoney();
        //};
    }

    private void OnEnable()
    {
        Upgrade.Instace.OnBuyMoney += UpgradeMoney;
    }

    private void OnDisable()
    {
        Upgrade.Instace.OnBuyMoney -= UpgradeMoney;
    }

    public int GetMoneyValue()
    {
        return _value;
    }

    private void UpgradeMoney()
    {
        _value += _upgradeMoney;

        PlayerPrefs.SetInt("money", _value);
    }
}
