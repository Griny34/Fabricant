using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _money;

    private int _salary = 0;
    private int _counter = 0;

    public event Action<int> OnMoneyChanged;
    public event Action<int> OnSalaryChanged;

    public static Wallet Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        LoadMoney();
    }

    public int GetSalary()
    {
        return _salary;
    }

    public void TakeSalary(int money)
    {
        _salary += money;

        OnSalaryChanged?.Invoke(money);
    }

    public void RestartSalary()
    {
        _salary = 0;

        OnSalaryChanged?.Invoke(0);
    }

    public int GetMoney()
    {
        OnSalaryChanged?.Invoke(_counter);
        return _money;
    }

    public void TakeMoney(int money)
    {
        _money += money;

        OnMoneyChanged?.Invoke(money);
    }

    public void GiveMoney(int money)
    {
        _money -= money;

        OnMoneyChanged?.Invoke(money);
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt("Money", _money);
    }

    public void LoadMoney()
    {
        if (PlayerPrefs.HasKey("Money") == false)
            return;

        int money = PlayerPrefs.GetInt("Money");

        _money = money;

        OnMoneyChanged?.Invoke(money);
    }
}