using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSourcePlay;
    [SerializeField] private AudioClip SoundButtonPlay;

    [SerializeField] private AudioSource AudioSourceOther;
    [SerializeField] private AudioClip SoundButtonOther;

    public void ClickSoundButtonPlay()
    {
        AudioSourcePlay.PlayOneShot(SoundButtonPlay);
    }

    public void ClickSoundOther()
    {
        AudioSourceOther.PlayOneShot(SoundButtonOther);
    }
}
