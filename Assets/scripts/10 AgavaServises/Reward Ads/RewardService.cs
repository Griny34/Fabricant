using Gameplay.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardService : MonoBehaviour
{
    [SerializeField] private TriggerHandler _triggerHandler;
    [SerializeField] private SpawnerMoney _spawnerMoney;
    [SerializeField] private ControlerPause _controlerPause;

    private string _keyVolume = "Volume";

    private int _priceViewing = 5;

    public void ShowRewardAds()
    {
        if (Agava.WebUtility.WebApplication.IsRunningOnWebGL == false)
        {
            
            return;
        }


        if (Agava.WebUtility.AdBlock.Enabled == true)
        {
           
            return;
        }


        Agava.YandexGames.VideoAd.Show(OnOpenColbek, AddMoney, OnCloseColbek);
    }

    private void OnOpenColbek()
    {
        _triggerHandler.gameObject.SetActive(false);
        _controlerPause.StopGame();
    }

    private void AddMoney()
    {        
        for (int i = 0; i < _priceViewing; i++)
        {
            _spawnerMoney.CreateMoney();       
        }
    }

    private void OnCloseColbek()
    {
        _controlerPause.PlayGame();
    }
}
