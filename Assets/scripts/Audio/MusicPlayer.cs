using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private Sound[] _sounds;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private string _keyVolume;

    private void Awake()
    {
        float volume = 1;

        if (PlayerPrefs.HasKey(_keyVolume))
        {
            volume = PlayerPrefs.GetFloat(_keyVolume);
        }

        foreach (Sound sound in _sounds)
        {
            sound.Source = _audioSource;
            sound.Source.clip = sound.Clip;

            sound.Source.volume = sound.Volume * volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }
    }

    private void Start()
    {
        if(_sounds.Length != 0)
        {
            _audioSource.clip = _sounds[0].Clip;

            _audioSource.Play();

            //_sounds[0].Source.Play();

            //Sound sound = _sounds[0];

            //sound.Source.Play();
        }        
    }

    public void ChangeVolume(float value)
    {
        _audioSource.volume = value;
    }
}
