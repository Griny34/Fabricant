using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstishelService : MonoBehaviour
{
    private const string _keyVolume = "Volume";

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
        Time.timeScale = 0;
        AudioListener.volume = 0f;
    }

    private void OnCloseColbek(bool isClosed)
    {
        Time.timeScale = 1;      

        if (PlayerPrefs.HasKey(_keyVolume))
        {
            AudioListener.volume = PlayerPrefs.GetFloat(_keyVolume);
        }
        else
        {
            AudioListener.volume = 1f;
        }
    }
}
