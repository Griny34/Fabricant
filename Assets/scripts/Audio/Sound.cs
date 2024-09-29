using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField ] private string Name;
    [SerializeField] private AudioClip Clip;

    [Range(0f, 1f)]
    [SerializeField] private float Volume;
    [Range(1f, 3f)]
    [SerializeField] private float Pitch;
    [SerializeField] private bool Loop;

    [HideInInspector]
    [SerializeField] private AudioSource Source;

    public AudioClip GetMyAudioClip()
    {
        return Clip;
    }
}
