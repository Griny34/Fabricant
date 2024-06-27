using System.Collections;
using System.Collections.Generic;
using Plugins.Audio.Core;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private Sound[] _sounds;
    [SerializeField] private SourceAudio  _sourceAudio;
    [SerializeField] private string _keyVolume;

    private float _volumeOnStart = 1;

    private void Awake()
    {       
        if (PlayerPrefs.HasKey(_keyVolume))
        {
            _sourceAudio.Volume = PlayerPrefs.GetFloat(_keyVolume);
        }
        else
        {
            _sourceAudio.Volume = _volumeOnStart;
        }



        //foreach (Sound sound in _sounds)
        //{
        //    sound.Source = _sourceAudio;
        //    sound.Source.clip = sound.Clip;

        //    sound.Source.volume = sound.Volume * _sourceAudio.Volume;
        //    sound.Source.pitch = sound.Pitch;
        //    sound.Source.loop = sound.Loop;
        //}
    }

    private void Start()
    {
        if(_sounds.Length != 0)
        {
            _sourceAudio.Play(_sounds[0].Clip.name);

            //_sounds[0].Source.Play();

            //Sound sound = _sounds[0];

            //sound.Source.Play();
        }        
    }

    public void ChangeVolume(float value)
    {
        _sourceAudio.Volume = value;
        //AudioManagement.Instance.SetVolume(value);
    }
}
