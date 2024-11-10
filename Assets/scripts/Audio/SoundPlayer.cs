using Plugins.Audio.Core;
using UnityEngine;

namespace SoundAccompaniment
{
    public class SoundPlayer : MonoBehaviour
    {
        private const string _button = "Button";
        private const string _buttonNegation = "niht";

        [SerializeField] private SourceAudio _sourceAudio;

        public void ClickSoundButtonPlay()
        {
            _sourceAudio.PlayOneShot(_button);
        }

        public void ClickSoundOther()
        {
            _sourceAudio.PlayOneShot(_buttonNegation);
        }
    }
}