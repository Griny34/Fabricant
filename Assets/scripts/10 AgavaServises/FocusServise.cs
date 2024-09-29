using Agava.WebUtility;
using Plugins.Audio.Core;
using UnityEngine;

public class FocusServise : MonoBehaviour
{
    //[SerializeField] private PauseController _controlerPause;
    [SerializeField] private InterstitialService _interstishelService;
    [SerializeField] private RewardService _rewardService;

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
        MuteAudio(!inFocuse);
        PauseGame(!inFocuse);
    }

    private void OnInBakgroundChangeWeb(bool outFocuse)
    {
        MuteAudio(outFocuse);
        PauseGame(outFocuse);
    }

    private void MuteAudio(bool value)
    {
        if (_interstishelService.GetFlagAds() == false && _rewardService.GetFlagAds() == false)
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