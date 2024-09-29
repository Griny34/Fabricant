using UnityEngine;

public class JoystickPlayer : MonoBehaviour
{
    private const string _keyPrefsSpeed = "Speed";
    private const string _animationVelocity = "Velocity";
    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";

    [SerializeField] private bool _isMobile;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private DynamicJoystick _joystickLandscape;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _speed;
    [SerializeField] private float _upgradeSpeed;
    [SerializeField] private float _speedRotation;

    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;


    private int _velocityHash = 0;
    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(_keyPrefsSpeed))
        {
            _speed = PlayerPrefs.GetFloat(_keyPrefsSpeed);
        }
       
        _rigidbody = GetComponent<Rigidbody>();

        _velocityHash = Animator.StringToHash(_animationVelocity);

        MatchModel.Instace.OnFinished += () =>
        {
            transform.position = _startPosition;
        };

        Upgrade.Instace.OnBuySpeedPlayer += () =>
        {
            _speed += _upgradeSpeed;

            PlayerPrefs.SetFloat(_keyPrefsSpeed, _speed);
        };       
    }

    private void FixedUpdate()
    {
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
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis(_horizontal);
        float vertical = Input.GetAxis(_vertical);

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (direction.magnitude > Mathf.Abs(0.01f))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _speedRotation);
        }

        _animator.SetFloat(_velocityHash, _rigidbody.velocity.magnitude);
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
        
        if (_rigidbody.velocity.magnitude <= 0.05f)
        {
            _rigidbody.angularVelocity = Vector3.zero;
        }
        else
        {
            Rotate(direction);
        }
    }

    private void Rotate( Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.1f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        _rigidbody.MoveRotation(targetRotation);
    }
}