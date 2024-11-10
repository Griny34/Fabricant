using TMPro;
using UnityEngine;

namespace WalletUser
{
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

        private void OnEnable()
        {
            _wallet.OnMoneyChanged += ChangeMoneyText;
            _wallet.OnSalaryChanged += ChangeSalaryText;
        }

        private void OnDisable()
        {
            _wallet.OnMoneyChanged -= ChangeMoneyText;
            _wallet.OnSalaryChanged -= ChangeSalaryText;
        }

        private void ChangeMoneyText(int number)
        {
            _money.text = _wallet.GetMoney().ToString();
            _money2.text = _wallet.GetMoney().ToString();
        }

        private void ChangeSalaryText(int number)
        {
            _salary.text = _wallet.GetSalary().ToString();
            _salary2.text = _wallet.GetSalary().ToString();
        }
    }
}