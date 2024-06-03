using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private UnityEvent onFinished;
    [SerializeField] private UnityEvent onStarted;

    protected Transform _target;

    public event UnityAction OnStarted
    {
        add => onFinished.AddListener(value);
        remove => onFinished.RemoveListener(value);
    }

    public event UnityAction OnFinished
    {
        add => onFinished.AddListener(value);
        remove => onFinished.RemoveListener(value);
    }

    private void Update()
    {
        Move();

        if (_target == null) return;

        if (_target.position == transform.position)
        {
            onFinished?.Invoke();
            onFinished.RemoveAllListeners();

            _target = null;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out PointOff pointOff))
        {
            SwitchOffDesk();
        }
    }

    public void StartMove(Transform target)
    {
        _target = target;

        onStarted?.Invoke();
        onStarted.RemoveAllListeners();
    }

    private void Move()
    {
        if (_target == null) return;

        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    private void SwitchOffDesk()
    {
        gameObject.SetActive(false);
    }
}
