using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementPlayer;

[RequireComponent(typeof(Rigidbody))]
public class MovementPlayer : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private float _upgradeSpeed;

    private Rigidbody _rigidbody;
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _startPosition;

    public MovementStates MovementState { get; private set; }

    public static MovementPlayer Instance { get; private set; }
    public float MovmentSpeed => _rigidbody.velocity.magnitude;

    public event Action<MovementStates> MovemetStateChanged;    

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;

        MovementState = MovementStates.Idle;

        _startPosition = transform.position;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        MatchModel.Instace.OnFinished += () =>
        {
            transform.position = _startPosition;
        };

        Upgrade.Instace.OnBuySpeedPlayer += () =>
        {
            _speed += _upgradeSpeed;
        };
    }


    private void Update()
    {
        _horizontalInput = Input.GetAxis(Horizontal);
        _verticalInput = Input.GetAxis(Vertical);

        Vector3 direction = new Vector3(_horizontalInput, 0f, _verticalInput).normalized * _speed;

        direction.y = _rigidbody.velocity.y;

        _rigidbody.velocity = direction;

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            _rigidbody.rotation = rotation;

            if(MovementState != MovementStates.Run)
            {
                MovemetStateChanged?.Invoke(MovementStates.Run);
                MovementState = MovementStates.Run;
            }
        }
        else
        {
            if(MovementState != MovementStates.Idle)
            {
                MovemetStateChanged?.Invoke(MovementStates.Idle);
                MovementState = MovementStates.Idle;
            }
        }       
    }

    public enum MovementStates
    {
        Idle = 0,
        Run = 1,
    }
}
