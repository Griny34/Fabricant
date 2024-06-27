using Plugins.Audio.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstishelService : MonoBehaviour
{
    [SerializeField] private ControlerPause _controlerPause;

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
            

        Agava.YandexGames.InterstitialAd.Show(OnOpenColbek,(isClosed) => 
        {
            OnCloseColbek(isClosed);
            onCloseCallBack.Invoke();
        });
    }

    private void OnOpenColbek()
    {
        AudioPauseHandler.Instance.PauseAudio();
        //Time.timeScale = 0;
        //_audioSource.Pause();

        //AudioListener.volume = 0f;
    }

    private void OnCloseColbek(bool isClosed)
    {
        AudioPauseHandler.Instance.UnpauseAudio();
        //Time.timeScale = 1;

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
}
