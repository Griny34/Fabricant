using TMPro;
using UnityEngine;

namespace UpgradeSkills
{
    public class ViewPurchases : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textNumberAttemptsBuySpeed;
        [SerializeField] private TextMeshProUGUI _textNumberAttemptsBuyMateriale;
        [SerializeField] private TextMeshProUGUI _textNumberAttemptsBuyMoney;

        [SerializeField] private Upgrade _upgrade;

        private void OnEnable()
        {
            _upgrade.OnBuySpeedPlayer += ChangeTextSpeed;
            _upgrade.OnBuyMoney += ChangeTextMoney;
            _upgrade.OnBuyDeskInventory += ChangeTextMaterialStack;
        }

        private void OnDisable()
        {
            _upgrade.OnBuySpeedPlayer -= ChangeTextSpeed;
            _upgrade.OnBuyMoney -= ChangeTextMoney;
            _upgrade.OnBuyDeskInventory -= ChangeTextMaterialStack;
        }

        private void ChangeTextSpeed()
        {
            _textNumberAttemptsBuySpeed.text = _upgrade.CountPaySpeed.ToString();
        }

        private void ChangeTextMoney()
        {
            _textNumberAttemptsBuyMoney.text = _upgrade.CountPayMoney.ToString();
        }

        private void ChangeTextMaterialStack()
        {
            _textNumberAttemptsBuyMateriale.text = _upgrade.CountPayDesk.ToString();
        }
    }
}