using Gameplay.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public static Upgrade Instace { get; private set; }

    [SerializeField] private SoundPlayer _soundPlayer;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private int _pretiumUpgradeSpeedPlayer;
    public int CountPaySpeed { get; private set; } = 0;
    [SerializeField] private int _maxPaySpeed;

    [SerializeField] private int _pretiumUpgradeDeskInventory;
    public int CountPayDesk { get; private set; } = 0;
    [SerializeField] private int _maxPayDesk;

    [SerializeField] private int _pretiumUpgradeChairInventory;
    public int CountPayChair { get; private set; } = 0;
    [SerializeField] private int _maxPayChair;

    [SerializeField] private int _pretiumUpgradeMoney;
    public int CountPayMoney { get; private set; } = 0;
    [SerializeField] private int _maxPayMoney;

    public event Action OnBuySpeedPlayer;
    public event Action OnBuyDeskInventory;
    public event Action OnBuyChairInventory;
    public event Action OnBuyMoney;

    public event Action OnCanNotBuySpeed;
    public event Action OnCanNotBayDesk;
    public event Action OnCanNotBayChair;
    public event Action OnCanNotBuyMoney;

    private void Awake()
    {
        if(Instace != null)
        {
            Destroy(gameObject);
            return;
        }

        Instace = this;
    }

    public void BuyUpgradeSpeedPlayer()
    {
        if(_wallet.GetMoney() >= _pretiumUpgradeSpeedPlayer && CountPaySpeed < _maxPaySpeed)
        {
            _soundPlayer.ClickSoundButtonPlay();
            _wallet.GiveMoney(_pretiumUpgradeSpeedPlayer);
            CountPaySpeed++;
            OnBuySpeedPlayer?.Invoke();
            PlayerPrefs.SetInt("CountPaySpeed", CountPaySpeed);
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
            PlayerPrefs.SetInt("CountPayDesk", CountPayDesk);
        }
        else
        {
            OnCanNotBayDesk?.Invoke();
            _soundPlayer.ClickSoundOther();
        }  
    }

    public void BuyUpgrateChairInventory()
    {
        if(_wallet.GetMoney() >= _pretiumUpgradeChairInventory && CountPayChair < _maxPayChair)
        {
            _soundPlayer.ClickSoundButtonPlay();
            _wallet.GiveMoney(_pretiumUpgradeChairInventory);
            CountPayChair++;
            OnBuyChairInventory?.Invoke();
            PlayerPrefs.SetInt("CountPayChair", CountPayChair);
        }
        else
        {
            OnCanNotBayChair?.Invoke();
            _soundPlayer.ClickSoundOther();
        }
    }

    public void BuyUpgrateMoney()
    {
        if(_wallet.GetMoney() >= _pretiumUpgradeMoney && CountPayMoney < _maxPayMoney)
        {
            _soundPlayer.ClickSoundButtonPlay();
            _wallet.GiveMoney(_pretiumUpgradeMoney);
            CountPayMoney++;
            OnBuyMoney?.Invoke();
            PlayerPrefs.SetInt("CountPayMoney", CountPayMoney);
        }
        else
        {
            OnCanNotBuyMoney?.Invoke();
            _soundPlayer.ClickSoundOther();
        }
    }

    public void LoadTryes()
    {
        CountPaySpeed = PlayerPrefs.GetInt("CountPaySpeed");
        CountPayDesk = PlayerPrefs.GetInt("CountPayDesk");
        CountPayChair = PlayerPrefs.GetInt("CountPayChair");
        CountPayMoney = PlayerPrefs.GetInt("CountPayMoney");
    }
}
