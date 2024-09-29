using Plugins.Audio.Core;
using System;
using UnityEngine;

public class InterstitialService : MonoBehaviour
{
    [SerializeField] private PauseController _controlerPause;
    [SerializeField] private MatchModel _matchModel;

    private bool _isPlayInterstishel = false;

    public void ShowInterstitial(Action onCloseCallBack)
    {
        if (Agava.WebUtility.WebApplication.IsRunningOnWebGL == false)
        {
            onCloseCallBack.Invoke();
            return;
        }


        if (Agava.WebUtility.AdBlock.Enabled == true)
        {
            onCloseCallBack.Invoke();
            return;
        }

        Agava.YandexGames.InterstitialAd.Show(OnOpenColbek, OnCloseColbek);
        {
            onCloseCallBack.Invoke();
        };
    }

    private void OnOpenColbek()
    {
        _isPlayInterstishel = true;
        AudioPauseHandler.Instance.PauseAudio();
        Time.timeScale = 0;
    }

    private void OnCloseColbek(bool isClosed)
    {
        _isPlayInterstishel = false;

        _matchModel.OnMatchChanged?.Invoke();

        AudioPauseHandler.Instance.UnpauseAudio();

        Time.timeScale = 1;
    }

    public bool GetFlagAds()
    {
        return _isPlayInterstishel;
    }
}