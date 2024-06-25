using Agava.WebUtility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FocusServise : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ControlerPause _controlerPause;

    private float _maxValue;

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

    private void OnInBakgroundChangeApp(bool inFocuse) =>
        _controlerPause.OutOfFocuse = !inFocuse;

        //MuteAudio(!app);
        //PauseGame(!app);
    

    private void OnInBakgroundChangeWeb(bool outFocuse) =>
        _controlerPause.OutOfFocuse = outFocuse;

        //MuteAudio(isBackGround);
        //PauseGame(isBackGround);
    

    //private void MuteAudio(bool value)
    //{
    //    if (value)
    //    {
    //        _controlerPause.StopGame();
    //    }
    //    else
    //    {
    //        _controlerPause.PlayGame();
    //    }
    //}

    //private void PauseGame(bool value)
    //{
    //    Time.timeScale = value ? 0 : 1;
    //}
}
