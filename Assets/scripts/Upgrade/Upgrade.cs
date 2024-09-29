using System;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private const string _countPaySpeed = "CountPaySpeed";
    private const string _countPayDesk = "CountPayDesk";
    private const string _countPayChair = "CountPayChair";
    private const string _countPayMoney = "CountPayMoney";

    [SerializeField] private SoundPlayer _soundPlayer;
    [SerializeField] private Wallet _wallet;

    [SerializeField] private int _pretiumUpgradeSpeedPlayer;
    [SerializeField] private int _maxPaySpeed;

    [SerializeField] private int _pretiumUpgradeDeskInventory;
    [SerializeField] private int _maxPayDesk;

    [SerializeField] private int _pretiumUpgradeChairInventory;
    [SerializeField] private int _maxPayChair;

    [SerializeField] private int _pretiumUpgradeMoney;
    [SerializeField] private int _maxPayMoney;

    public event Action OnBuySpeedPlayer;
    public event Action OnBuyDeskInventory;
    public event Action OnBuyChairInventory;
    public event Action OnBuyMoney;

    public event Action OnCanNotBuySpeed;
    public event Action OnCanNotBayDesk;
    public event Action OnCanNotBayChair;
    public event Action OnCanNotBuyMoney;

    public static Upgrade Instace { get; private set; }

    public int CountPaySpeed { get; private set; } = 0;

    public int CountPayDesk { get; private set; } = 0;

    public int CountPayChair { get; private set; } = 0;

    public int CountPayMoney { get; private set; } = 0;

    private void Awake()
    {
        if (Instace != null)
        {
            Destroy(gameObject);
            return;
        }

        Instace = this;
    }

    public void BuyUpgradeSpeedPlayer()
    {
        if (_wallet.GetMoney() >= _pretiumUpgradeSpeedPlayer && CountPaySpeed < _maxPaySpeed)
        {
            _soundPlayer.ClickSoundButtonPlay();
            _wallet.GiveMoney(_pretiumUpgradeSpeedPlayer);
            CountPaySpeed++;
            OnBuySpeedPlayer?.Invoke();
            PlayerPrefs.SetInt(_countPaySpeed, CountPaySpeed);
        }
        else
        {
            _soundPlayer.ClickSoundOther();
            OnCanNotBuySpeed?.Invoke();
        }
    }

    public void BuyUpgrateDeskInventory()
    {
        if (_wallet.GetMoney() >= _pretiumUpgradeSpeedPlayer && CountPayDesk < _maxPayDesk)
        {
            _soundPlayer.ClickSoundButtonPlay();
            _wallet.GiveMoney(_pretiumUpgradeDeskInventory);
            CountPayDesk++;
            OnBuyDeskInventory?.Invoke();
            PlayerPrefs.SetInt(_countPayDesk, CountPayDesk);
        }
        else
        {
            OnCanNotBayDesk?.Invoke();
            _soundPlayer.ClickSoundOther();
        }  
    }

    public void BuyUpgrateChairInventory()
    {
        if (_wallet.GetMoney() >= _pretiumUpgradeChairInventory && CountPayChair < _maxPayChair)
        {
            _soundPlayer.ClickSoundButtonPlay();
            _wallet.GiveMoney(_pretiumUpgradeChairInventory);
            CountPayChair++;
            OnBuyChairInventory?.Invoke();
            PlayerPrefs.SetInt(_countPayChair, CountPayChair);
        }
        else
        {
            OnCanNotBayChair?.Invoke();
            _soundPlayer.ClickSoundOther();
        }
    }

    public void BuyUpgrateMoney()
    {
        if (_wallet.GetMoney() >= _pretiumUpgradeMoney && CountPayMoney < _maxPayMoney)
        {
            _soundPlayer.ClickSoundButtonPlay();
            _wallet.GiveMoney(_pretiumUpgradeMoney);
            CountPayMoney++;
            OnBuyMoney?.Invoke();
            PlayerPrefs.SetInt(_countPayMoney, CountPayMoney);
        }
        else
        {
            OnCanNotBuyMoney?.Invoke();
            _soundPlayer.ClickSoundOther();
        }
    }

    public void LoadTryes()
    {
        CountPaySpeed = PlayerPrefs.GetInt(_countPaySpeed);
        CountPayDesk = PlayerPrefs.GetInt(_countPayDesk);
        CountPayChair = PlayerPrefs.GetInt(_countPayChair);
        CountPayMoney = PlayerPrefs.GetInt(_countPayMoney);

        OnBuySpeedPlayer?.Invoke();
        OnBuyDeskInventory?.Invoke();
        OnBuyChairInventory?.Invoke();
        OnBuyMoney?.Invoke();
    }
}
