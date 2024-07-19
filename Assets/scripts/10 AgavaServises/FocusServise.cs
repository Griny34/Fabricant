using Agava.WebUtility;
using Plugins.Audio.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FocusServise : MonoBehaviour
{
    [SerializeField] private ControlerPause _controlerPause;
    [SerializeField] private InterstishelService _interstishelService;
    [SerializeField] private RewardService _rewardService;

    //private float _maxValue;

    private void OnEnable()
    {
        if (WebApplication.IsRunningOnWebGL == false)
            return; 

        Application.focusChanged += OnInBakgroundChangeApp;
        WebApplication.InBackgroundChangeEvent += OnInBakgroundChangeWeb;
    }

    private void OnDisable()
    {
        if (WebApplication.IsRunningOnWebGL == false)
            return;

        Application.focusChanged -= OnInBakgroundChangeApp;
        WebApplication.InBackgroundChangeEvent -= OnInBakgroundChangeWeb;
    }

    private void OnInBakgroundChangeApp(bool inFocuse)
    {
        //_controlerPause.OutOfFocuse = !inFocuse;

        MuteAudio(!inFocuse);
        PauseGame(!inFocuse);
    }

    private void OnInBakgroundChangeWeb(bool outFocuse)
    {
        //_controlerPause.OutOfFocuse = outFocuse;
        MuteAudio(outFocuse);
        PauseGame(outFocuse);
    }
        
    private void MuteAudio(bool value)
    {
        if(_interstishelService.GetFlagAds() == false && _rewardService.GetFlagAds() == false)
        {
            if (value)
            {
                AudioPauseHandler.Instance.PauseAudio();
            }
            else
            {
                AudioPauseHandler.Instance.UnpauseAudio();
            }
        }
    }

    private void PauseGame(bool value)
    {
        if (_interstishelService.GetFlagAds() == false && _rewardService.GetFlagAds() == false)
        {
            Time.timeScale = value ? 0 : 1;
        }           
    }
}
