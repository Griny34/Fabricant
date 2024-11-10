using System;
using UnityEngine;

namespace WalletUser
{
    public class Wallet : MonoBehaviour
    {
        private const string _hashMoneyKey = "Money";

        [SerializeField] private int _money;

        private int _salary = 0;
        private int _counter = 0;

        public event Action<int> OnMoneyChanged;
        public event Action<int> OnSalaryChanged;

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
            PlayerPrefs.SetInt(_hashMoneyKey, _money);
        }

        public void LoadMoney()
        {
            if (PlayerPrefs.HasKey(_hashMoneyKey) == false)
                return;

            _money = PlayerPrefs.GetInt(_hashMoneyKey);

            OnMoneyChanged?.Invoke(_money);
        }
    }
}