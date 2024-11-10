using UnityEngine;
using UpgradeSkills;

namespace Currency
{
    public class Money : MonoBehaviour
    {
        private const string _keyPrefsMoney = "money";

        [SerializeField] private int _value;
        [SerializeField] private int _upgradeMoney;
        [SerializeField] private Upgrade _upgrade;

        private int _startMoney = 10;

        private void Start()
        {
            if (PlayerPrefs.HasKey(_keyPrefsMoney))
            {
                _value = PlayerPrefs.GetInt(_keyPrefsMoney);
            }
            else
            {
                _value = _startMoney;
            }
        }

        private void OnEnable()
        {
            _upgrade.OnBuyMoney += UpgradeMoney;
        }

        private void OnDisable()
        {
            _upgrade.OnBuyMoney -= UpgradeMoney;
        }

        public int GetMoneyValue()
        {
            return _value;
        }

        private void UpgradeMoney()
        {
            _value += _upgradeMoney;

            PlayerPrefs.SetInt(_keyPrefsMoney, _value);
        }
    }
}