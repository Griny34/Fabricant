using TMPro;
using UnityEngine;
using WalletUser;

public class MatchView : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] private MatchModel _matchModel;
    [SerializeField] private Wallet _wallet;

    //[Header("UIPlayer")]
    //[SerializeField] private TextMeshProUGUI _playerProgress;

    private void OnEnable()
    {
        _wallet.OnMoneyChanged += UpdateUI;
    }

    private void OnDisable()
    {
        _wallet.OnMoneyChanged -= UpdateUI;
    }

    private void UpdateUI(int money)
    {
        //_playerProgress.text = _wallet.GetMoney().ToString();
    }
}
