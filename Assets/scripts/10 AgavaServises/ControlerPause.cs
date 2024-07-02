using Plugins.Audio.Core;
using UnityEngine;

public class ControlerPause : MonoBehaviour
{
    [SerializeField] private SourceAudio _sourceAudio;

    private bool _isPaused;
    private bool _outOfFocuse;

    public bool IsPaused
    {
        get => _isPaused;

        set
        {
            _isPaused = value;
            HandlePause();
        }
    }

    public bool OutOfFocuse
    {
        get => _outOfFocuse;

        set
        {
            _outOfFocuse = value;
            HandlePause();
        }
    }

    public void HandlePause()
    {
        if(_isPaused == false && _outOfFocuse == false)
        {
            _sourceAudio.UnPause();
            Time.timeScale = 1f;
        }
        else
        {
            _sourceAudio.Pause();
            Time.timeScale = 0f;
        }

        if(_outOfFocuse == false)
            _sourceAudio.UnPause();
    }

    private void Update()
    {
        //Debug.Log(IsPaused + "  IsPaused");
        //Debug.Log(OutOfFocuse + "  OutOfFocuse");
    }
    public void StopGame()
    {

    }

    public void StopTime()
    {

    }

    public void PlayGame()
    {

    }
}
