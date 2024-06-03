using Gameplay.Common;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Car : MonoBehaviour
{
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


    private List<Furniture> _chairs = new List<Furniture>();
    private Furniture _relevantFurniture;
    private Coroutine _corutine;
    private Coroutine _corutineChair;
    private int _countChair;

    public static Car Instance { get; private set;}

    private void Start()
    {
        _carAria.OnEnter += (col) =>
        {
            if(_stackFurniture.GetListStack().Count == 0) return;

            if (col.GetComponent<JoystickPlayer>() == null) return;

            if (_stackFurniture.GetFurniture() == null || _stackFurniture.GetFurniture().GetName() != _ordersSpawner.RelevantOrder().GetName())
            {
                //Text
                return;
            }

            _ordersSpawner.DestroyOrder();

            if (_corutineChair != null)
            {
                StopCoroutine(_corutineChair);
            }

            _corutineChair = StartCoroutine(MoveChair());
        };

        _carAria.OnExit += (col) =>
        {
            if(_chairs.Count == 0) return;

            if (_corutine != null)
            {
                StopCoroutine(_corutine);
            }

            _corutine = StartCoroutine(MoveCarCoroutine());            
        };
    }

    private IEnumerator MoveChair()
    {
        while (_stackFurniture.GetListStack().Count != 0)
        {
            _relevantFurniture = _stackFurniture.GetFurniture();

            //if (_relevantChair == null) return;
            _stackFurniture.RemoveFurniture(_relevantFurniture, _startPosition);

            _chairs.Add(_relevantFurniture);

            yield return new WaitForSeconds(1f);

            _countChair = _relevantFurniture.GiveVolumePrice();

            yield return null;
        }
    }

    private IEnumerator MoveCarCoroutine()
    {
        DestroyChair();

        _chairs.Clear();

        yield return new WaitForSeconds(0.6f);

        while (transform.position != _positionShop.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _positionShop.position, _speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        while (transform.position != _startPosition.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPosition.position, _speed * Time.deltaTime);
            yield return null;
        }

        for(int i = 0; i < _countChair; i++)
        {
            _spawnerMoney.CreateMoney();
        }

        _countChair = 0;
    }

    private void DestroyChair()
    {
        if(Relevant() == null) return;

        Destroy(Relevant().gameObject);
    }

    private Furniture Relevant()
    {
        return _chairs[_chairs.Count - 1];
    }
}
