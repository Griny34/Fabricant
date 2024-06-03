using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Agava.WebUtility;
using UnityEngine.Animations;

public class JoystickPlayer : MonoBehaviour
{
    //private const string _isRun = "IsRun";

    [SerializeField] private bool _isMobile;
    [SerializeField] private Rigidbody _rigidbody;
    //[SerializeField] private FixedJoystick _joystickPortrait;
    [SerializeField] private DynamicJoystick _joystickLandscape;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _speed;
    [SerializeField] private float _upgradeSpeed;
    [SerializeField] private float _speedRotation;

    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;

    private float _velocity = 0;
    private int _velocityHash = 0;

    private Vector3 _startPosition;
    //private bool _isRunning = false;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Speed"))
        {
            _speed = PlayerPrefs.GetFloat("Speed");
        }
       
        _rigidbody = GetComponent<Rigidbody>();

        _velocityHash = Animator.StringToHash("Velocity");


        MatchModel.Instace.OnFinished += () =>
        {
            transform.position = _startPosition;
        };

        Upgrade.Instace.OnBuySpeedPlayer += () =>
        {
            _speed += _upgradeSpeed;

            PlayerPrefs.SetFloat("Speed", _speed);
        };       
    }

    private void Update()
    {
        //if (Screen.width < Screen.height)
        //{
        //    MovePlayerAnimation(_joystickPortrait);
        //}
        //else
        //{
        //    MovePlayerAnimation(_joystickLandscape);
        //}
    }


    private void FixedUpdate()
    {
        //if (Screen.width < Screen.height)
        //{
        //    MovePlayer(_joystickPortrait);
        //}
        //else
        //{
#if !UNITY_EDITOR && UNITY_WEBGL
        _isMobile = Device.IsMobile;
#endif

        if (_isMobile)
        {
            _joystickLandscape.gameObject.SetActive(true);

            MovePlayer(_joystickLandscape);
        }
        else
        {
            _joystickLandscape.gameObject.SetActive(false);

            MovePlayer();
        }
            //MovePlayer();
        //}
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if(direction.magnitude > Mathf.Abs(0.01f))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction),Time.deltaTime * _speedRotation);
        }

        _animator.SetFloat(_velocityHash, _rigidbody.velocity.magnitude /*Vector3.ClampMagnitude(direction, 1).magnitude*/);
        _rigidbody.velocity = Vector3.ClampMagnitude(direction, 1) * _speed;

        if (_rigidbody.velocity.magnitude <= 0.05f)
        {
            _rigidbody.angularVelocity = Vector3.zero;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
    }

    private void MovePlayer(DynamicJoystick joystick)
    {
        _rigidbody.velocity = new Vector3(joystick.Horizontal * _speed, _rigidbody.velocity.y, joystick.Vertical * _speed);

        _animator.SetFloat(_velocityHash, _rigidbody.velocity.magnitude);

        Vector3 direction = _rigidbody.velocity;

        
        if(_rigidbody.velocity.magnitude <= 0.05f)
        {
            _rigidbody.angularVelocity = Vector3.zero;
        }
        else
        {
            Rotate(joystick, direction);
        }


        //transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);

        //MovePlayerAnimation(joystick);

        //if (joystick.Horizontal != 0 || joystick.Vertical != 0 && _isRunning == false)
        //{
        //    transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        //    _animator.SetBool(_isRun, true);
        //    _isRunning = true;
        //}
        //else
        //{
        //    _animator.SetBool(_isRun, false);
        //    _isRunning = false;
        //}
    }

    private void Rotate(DynamicJoystick joystick, Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.1f) 
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        _rigidbody.MoveRotation(targetRotation);

        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1);
    }

    private void MovePlayerAnimation(FixedJoystick joystick)
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0 && _velocity < 1)
        {         
            _velocity += Time.deltaTime * _acceleration;
        }

        if(joystick.Horizontal == 0 || joystick.Vertical == 0 && _velocity > 0)
        {
            _velocity -= Time.deltaTime * _deceleration;
        }

        if(joystick.Horizontal == 0 || joystick.Vertical == 0 && _velocity < 0)
        {
            _velocity = 0;
        }

        _animator.SetFloat(_velocityHash, _velocity);
    }
}
