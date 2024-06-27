using Plugins.Audio.Core;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private SourceAudio _sourceAudio;

    public void ClickSoundButtonPlay()
    {
        _sourceAudio.PlayOneShot("Button");
    }

    public void ClickSoundOther()
    {
        _sourceAudio.PlayOneShot("niht");
    }
}
