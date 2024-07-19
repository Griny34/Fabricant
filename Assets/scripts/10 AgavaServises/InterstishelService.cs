using Plugins.Audio.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstishelService : MonoBehaviour
{
    [SerializeField] private ControlerPause _controlerPause;

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

        //Agava.YandexGames.InterstitialAd.Show(OnOpenColbek, OnCloseColbek);

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

        Debug.Log(Time.timeScale + "Началась реклама");

        //_controlerPause.OutOfFocuse = true;
        //_audioSource.Pause();

        //AudioListener.volume = 0f;
    }

    private void OnCloseColbek(bool isClosed)
    {
        //if (!isClosed)
        //{
        //    _controlerPause.OutOfFocuse = false;
        //}

        _isPlayInterstishel = false;
        AudioPauseHandler.Instance.UnpauseAudio();

        Time.timeScale = 1;

        Debug.Log(Time.timeScale + "Кончилась реклама");

        //_audioSource.Play();

        //if (PlayerPrefs.HasKey(_keyVolume))
        //{
        //    AudioListener.volume = PlayerPrefs.GetFloat(_keyVolume);
        //}
        //else
        //{
        //    AudioListener.volume = 1f;
        //}
    }

    public bool GetFlagAds()
    {
        return _isPlayInterstishel;
    }
}
