using Plugins.Audio.Core;
using UnityEngine;

namespace SoundAccompaniment
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private Sound[] _sounds;
        [SerializeField] private SourceAudio _sourceAudio;
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
        }

        private void Start()
        {
            if (_sounds.Length != 0)
            {
                _sourceAudio.Play(_sounds[0].GetMyAudioClip().name);

                _sourceAudio.Loop = true;
            }
        }

        public void ChangeVolume(float value)
        {
            _sourceAudio.Volume = value;
        }
    }
}