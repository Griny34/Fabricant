using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string _isRun = "IsRun";

    [SerializeField] private JoystickPlayer _movementPlayer;
    [SerializeField] private Animator _animator;

    //private bool _isRunning = false;

    private void OnEnable()
    {
        //_movementPlayer.MovemetStateChanged += OnMovemetStateChanged;
    }

    private void OnDisable()
    {
        //_movementPlayer.MovemetStateChanged -= OnMovemetStateChanged;
    }
    private void OnMovemetStateChanged(MovementPlayer.MovementStates state)
    {
        if (state == MovementPlayer.MovementStates.Run)
        {
            _animator.SetBool(_isRun, true);
        }
        else
        {
            _animator.SetBool(_isRun, false);
        }
    }

    private void Update()
    {
        //if (MovementPlayer.Instance.MovmentSpeed == 0 && _isRunning == true)
        //{
        //    _animator.SetBool(_isRun, false);
        //    _isRunning = false;
        //}

        //if (MovementPlayer.Instance.MovmentSpeed > 0 && _isRunning == false)
        //{
        //    _animator.SetBool(_isRun, true);
        //    _isRunning = true;
        //}
    }
}
