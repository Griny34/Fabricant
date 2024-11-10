using System.Collections;
using System.Collections.Generic;
using Gameplay.Common;
using UnityEngine;
using Orders;
using Player;
using Currency;

public class Car : MonoBehaviour
{
    private const float _delyeCoroutine = 1f;

    [Header("General properties")]
    [SerializeField] private int _maxCountChair;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _positionShop;

    [Header("Handlers")]
    [SerializeField] private TriggerHandler _carAria;
    [SerializeField] private StackFurniture _stackFurniture;
    [SerializeField] private OrdersSpawner _ordersSpawner;

    [SerializeField] private SpawnerMoney _spawnerMoney;
    [SerializeField] private int _countMoney;

    [SerializeField] private Transform _startPosition;

    private WaitForSeconds _timeCoroutine = new WaitForSeconds(_delyeCoroutine);
    private List<Furniture> _chairs = new List<Furniture>();
    private Furniture _relevantFurniture;
    private Coroutine _corutine;
    private Coroutine _corutineChair;
    private int _countChair;

    private void OnEnable()
    {
        _carAria.OnEnter += WorkEventEnter;
        _carAria.OnExit += WorkEventExit;
    }

    private void OnDisable()
    {
        _carAria.OnEnter -= WorkEventEnter;
        _carAria.OnExit -= WorkEventExit;
    }

    private void WorkEventEnter(Collider collider)
    {
        if (_stackFurniture.GetListStack().Count == 0)
            return;

        if (collider.GetComponent<JoystickPlayer>() == null)
            return;

        if (_stackFurniture.GetFurniture() == null
            || _stackFurniture.GetFurniture().GetName() != _ordersSpawner.RelevantOrder().GetName())
            return;

        _ordersSpawner.DestroyOrder();

        if (_corutineChair != null)
        {
            StopCoroutine(_corutineChair);
        }

        _corutineChair = StartCoroutine(MoveChair());
    }

    private void WorkEventExit(Collider collider)
    {
        if (_chairs.Count == 0)
            return;

        if (_corutine != null)
        {
            StopCoroutine(_corutine);
        }

        _corutine = StartCoroutine(MoveCarCoroutine());
    }

    private IEnumerator MoveChair()
    {
        while (_stackFurniture.GetListStack().Count != 0)
        {
            _relevantFurniture = _stackFurniture.GetFurniture();

            _stackFurniture.RemoveFurniture(_relevantFurniture, _startPosition);

            _chairs.Add(_relevantFurniture);

            yield return _timeCoroutine;

            _countChair = _relevantFurniture.GetVolumePrice();

            yield return null;
        }
    }

    private IEnumerator MoveCarCoroutine()
    {
        DestroyChair();

        _chairs.Clear();

        yield return _timeCoroutine;

        while (transform.position != _positionShop.position)
        {
            transform.position = Vector3
                .MoveTowards(
                transform.position,
                _positionShop.position,
                _speed * Time.deltaTime);
            yield return null;
        }

        yield return _timeCoroutine;

        while (transform.position != _startPosition.position)
        {
            transform.position = Vector3
                .MoveTowards(
                transform.position,
                _startPosition.position,
                _speed * Time.deltaTime);
            yield return null;
        }

        for (int i = 0; i < _countChair; i++)
        {
            _spawnerMoney.CreateMoney();
        }

        _countChair = 0;
    }

    private void DestroyChair()
    {
        if (Relevant() == null) return;

        Destroy(Relevant().gameObject);
    }

    private Furniture Relevant()
    {
        return _chairs[_chairs.Count - 1];
    }
}