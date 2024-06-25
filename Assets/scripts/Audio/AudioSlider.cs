using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Slider _slider2;
    [SerializeField] private string _keyVolume;
    [SerializeField] private MusicPlayer _musicPlayer;

    public event Action<float> VolumeChanged;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(_keyVolume))
        {
            _slider.value = PlayerPrefs.GetFloat(_keyVolume);
            _slider2.value = PlayerPrefs.GetFloat(_keyVolume);
        }
        else
        {
            _slider2.value = 1;
            _slider.value = 1;
        }
    }

    public void ChangeVolume(Slider slider)
    {
        if(_musicPlayer != null)
        {
            _musicPlayer.ChangeVolume(slider.value);
        }

        VolumeChanged?.Invoke(_slider.value);
        SaveVolume(slider);
    }

    //private void OnDestroy()
    //{
    //    PlayerPrefs.SetFloat(_keyVolume, _slider.value);
    //}

    public void SaveVolume(Slider slider)
    {
        PlayerPrefs.SetFloat(_keyVolume, slider.value);
    }
}
