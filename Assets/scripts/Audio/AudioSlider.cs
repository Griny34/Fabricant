using System;
using UnityEngine;
using UnityEngine.UI;

namespace SoundAccompaniment
{
    public class AudioSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Slider _sliderUnderstudy;
        [SerializeField] private string _keyVolume;
        [SerializeField] private MusicPlayer _musicPlayer;

        public event Action<float> VolumeChanged;

        private void Awake()
        {
            if (PlayerPrefs.HasKey(_keyVolume))
            {
                _slider.value = PlayerPrefs.GetFloat(_keyVolume);
                _sliderUnderstudy.value = PlayerPrefs.GetFloat(_keyVolume);
            }
            else
            {
                _slider.value = 1;
                _sliderUnderstudy.value = 1;
            }
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(ChangeVolume);
            _sliderUnderstudy.onValueChanged.AddListener(ChangeVolume);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(ChangeVolume);
            _sliderUnderstudy.onValueChanged.RemoveListener(ChangeVolume);
        }

        public void ChangeVolume(float sliderValue)
        {
            if (_musicPlayer != null)
            {
                _musicPlayer.ChangeVolume(sliderValue);
            }

            VolumeChanged?.Invoke(_slider.value);
            SaveVolume(sliderValue);
        }

        public void SaveVolume(float sliderValue)
        {
            PlayerPrefs.SetFloat(_keyVolume, sliderValue);
        }
    }
}