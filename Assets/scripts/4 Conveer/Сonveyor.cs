using Gameplay.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Сonveyor : MonoBehaviour
{
    [SerializeField] private TriggerHandler _playerTrigger;
    [SerializeField] private JoystickPlayer _player;
    [SerializeField] private StackMaterial _stack;

    [SerializeField] private int _capasity;

    [SerializeField] private Board _prefabDesk;
    [SerializeField] private Transform _pointSpawner;
    [SerializeField] private float _delay;

    private float _elepsedTime = 0;

    private List<Board> _pool = new List<Board>();
    private Board _relevantDesk;
    private Coroutine _coroutine;

    public event Action OnFull;

    private void Start()
    {
        _playerTrigger.OnEnter += col =>
        {
            if (col.GetComponent<JoystickPlayer>() == null) return;

            OutDesk();
        };

        _playerTrigger.OnExit += col =>
        {
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        };

        Initialize();
    }
    private void Update()
    {
        _elepsedTime += Time.deltaTime;

        if (_elepsedTime >= _delay)
        {
            if (TryGetGameObject(out Board desk))
            {
                _elepsedTime = 0;

                SetDesk(desk, _pointSpawner.position);
            }
        }
    }

    private void SetDesk(Board desk, Vector3 spawnPosition)
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
        Board spawned = Instantiate(_prefabDesk, _pointSpawner);

        spawned.gameObject.SetActive(false);

        _pool.Add(spawned);
    }

    private bool TryGetGameObject(out Board result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result;
    }

    private Board GetRelevantDesk()
    {
        if (_pool.Count == 0)
        {
            return null;
        }

        float minDistance = Mathf.Infinity;
        int nearestDeskIndex = 0;
        int indexCounter = 0;

        foreach (Board desk in _pool)
        {
            float distance = Vector3.Distance(desk.transform.position, _player.transform.position);

            if (distance < minDistance)
            {
                nearestDeskIndex = indexCounter;
                minDistance = distance;
            }

            indexCounter++;
        }

        return _pool[nearestDeskIndex];
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
            _relevantDesk = GetRelevantDesk();
            _relevantDesk.GetComponent<StartMovementDesk>().enabled = false;
            _stack.AddMaterial(_relevantDesk);

            _pool.Remove(_relevantDesk);

            Spawn();
            yield return new WaitForSeconds(0.3f);
        }

        OnFull?.Invoke();
    }
}
