using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerPause : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

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

    private void HandlePause()
    {
        if(_isPaused == false && _outOfFocuse == false)
        {
            _audioSource.UnPause();
            Time.timeScale = 1f;
        }
        else
        {
            _audioSource.Pause();
            Time.timeScale = 0f;
        }

        if(_outOfFocuse == false)
            _audioSource.UnPause();
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
