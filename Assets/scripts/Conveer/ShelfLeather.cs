using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Common;
using Factory;
using UnityEngine;
using Player;

public class ShelfLeather : MonoBehaviour
{
    private const float _delyeCoroutine = 0.5f;

    [SerializeField] private TriggerHandler _playerTrigger;
    [SerializeField] private JoystickPlayer _player;
    [SerializeField] private StackMaterial _stack;

    [SerializeField] private int _capasity;

    [SerializeField] private Leather _prefabLeather;
    [SerializeField] private Transform _pointSpawner;
    [SerializeField] private float _delay;

    private float _elepsedTime = 0;
    private WaitForSeconds _timeCoroutine = new WaitForSeconds(_delyeCoroutine);
    private List<Leather> _pool = new List<Leather>();
    private Leather _relevantLeather;
    private Coroutine _coroutine;

    public event Action OnFilled;

    private void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        _playerTrigger.OnEnter += WorkEventEnter;
        _playerTrigger.OnExit += WorkEventExit;
    }

    private void OnDisable()
    {
        _playerTrigger.OnEnter -= WorkEventEnter;
        _playerTrigger.OnExit -= WorkEventExit;
    }

    private void WorkEventEnter(Collider collider)
    {
        if (collider.GetComponent<JoystickPlayer>() == null)
            return;

        OutDesk();
    }

    private void WorkEventExit(Collider collider)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void Update()
    {
        _elepsedTime += Time.deltaTime;

        if (_elepsedTime >= _delay)
        {
            if (TryGetLeather(out Leather desk))
            {
                _elepsedTime = 0;

                SetDesk(desk, _pointSpawner.position);
            }
        }
    }

    private void SetDesk(Leather desk, Vector3 spawnPosition)
    {
        desk.gameObject.SetActive(true);
        desk.transform.position = spawnPosition;
    }

    private void Initialize()
    {
        for (int i = 0; i < _capasity; i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Leather spawned = Instantiate(_prefabLeather, _pointSpawner);

        spawned.gameObject.SetActive(false);

        _pool.Add(spawned);
    }

    private bool TryGetLeather(out Leather result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result;
    }

    private Leather GetRelevantDesk()
    {
        if (_pool.Count <= 0)
        {
            return null;
        }

        return _pool[_pool.Count - 1];
    }

    private void OutDesk()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(TakeDesk());
    }

    private IEnumerator TakeDesk()
    {
        while (!_stack.IsFull)
        {
            _relevantLeather = GetRelevantDesk();

            if(_relevantLeather != null)
            {
                _stack.AddMaterial(_relevantLeather);

                _pool.Remove(_relevantLeather);

                Spawn();

                yield return _timeCoroutine;
            }
        }

        OnFilled?.Invoke();
    }
}