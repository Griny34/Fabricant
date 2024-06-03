using Agava.WebUtility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FocusServise : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private float _maxValue;

    private void OnEnable()
    {
        if (Agava.WebUtility.WebApplication.IsRunningOnWebGL == false)
            return;

        Application.focusChanged += OnInBakgroundChangeApp;
        WebApplication.InBackgroundChangeEvent += OnInBakgroundChangeWeb;
    }

    private void OnDisable()
    {
        if (Agava.WebUtility.WebApplication.IsRunningOnWebGL == false)
            return;

        Application.focusChanged -= OnInBakgroundChangeApp;
        WebApplication.InBackgroundChangeEvent -= OnInBakgroundChangeWeb;
    }

    private void OnInBakgroundChangeApp(bool app)
    {
        MuteAudio(!app);
        PauseGame(!app);
    }

    private void OnInBakgroundChangeWeb(bool isBackGround)
    {
        MuteAudio(isBackGround);
        PauseGame(isBackGround);
    }

    private void MuteAudio(bool value)
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            _maxValue = PlayerPrefs.GetFloat("Volume");


        }
        else
        {

        }

        _audioSource.volume = value ? 0 : 1;
    }

    private void PauseGame(bool value)
    {
        Time.timeScale = value ? 0 : 1;
    }
}
