using Gameplay.Common;
using Plugins.Audio.Core;
using UnityEngine;

public class RewardService : MonoBehaviour
{
    [SerializeField] private TriggerHandler _triggerHandler;
    [SerializeField] private SpawnerMoney _spawnerMoney;
    //[SerializeField] private PauseController _controlerPause;

    private int _priceViewing = 5;
    private bool _isPlayReward = false;

    public void ShowRewardAds()
    {
        if (Agava.WebUtility.WebApplication.IsRunningOnWebGL == false)
            return;

        if (Agava.WebUtility.AdBlock.Enabled == true)
            return;

        Agava.YandexGames.VideoAd.Show(OnOpenColbek, AddMoney, OnCloseColbek);
    }

    private void OnOpenColbek()
    {
        _isPlayReward = true;
        _triggerHandler.gameObject.SetActive(false);
        AudioPauseHandler.Instance.PauseAudio();
        Time.timeScale = 0;
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
        _isPlayReward = false;
        AudioPauseHandler.Instance.UnpauseAudio();
        Time.timeScale = 1;
    }

    public bool GetFlagAds()
    {
        return _isPlayReward;
    }
}
