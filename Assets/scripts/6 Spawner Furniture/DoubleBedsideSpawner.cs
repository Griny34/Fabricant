using System;
using System.Collections;
using UnityEngine;

public class DoubleBedsideSpawner : SpawnerFurniture
{
    private const string _animationWork = "Work";

    [SerializeField] private DoubleBedsideTable _prefabDoubleBedsideTable;

    private float _delayCoroutine = 0.5f;
    private float _timeAnimation = 3f;
    private BedsideTable _bedsideTableRelevant;
    private BedsideTable _bedsideTable;
    private Coroutine _coroutineAcceptFurniture;

    public override event Action OnStartedEffect;
    public override event Action OnChangedCount;
    public override event Action OnChagedCountFurniture;

    private void Start()
    {
        //_triggerHandler.OnEnter += col =>
        //{
        //    if (col.GetComponent<JoystickPlayer>() == null) return;

        //    if (IsOpen == false) return;

        //    if (SearchBedsideTable() != null)
        //    {
        //        if (_coroutineAcceptFurniture != null)
        //        {
        //            StopCoroutine(_coroutineAcceptFurniture);
        //        }

        //        _coroutineAcceptFurniture = StartCoroutine(AcceptFurniture());
        //    }
        //};

        //_ariaSpawner.OnEnter += col =>
        //{
        //    if (_stackFurniture.IsFull == true) return;

        //    GivFurniture();
        //};
    }

    private void OnEnable()
    {
        _triggerHandler.OnEnter += WorkEventEnter;
        _triggerHandler.OnExit += WorkEventExit;
        _ariaSpawner.OnEnter += WorkEventGiveStool;
    }

    private void OnDisable()
    {
        _triggerHandler.OnEnter -= WorkEventEnter;
        _triggerHandler.OnExit -= WorkEventExit;
        _ariaSpawner.OnEnter -= WorkEventGiveStool;
    }

    private void WorkEventEnter(Collider collider)
    {
        if (collider.GetComponent<JoystickPlayer>() == null)
            return;

        if (IsOpen == false)
            return;

        if (SearchBedsideTable() != null)
        {
            if (_coroutineAcceptFurniture != null)
            {
                StopCoroutine(_coroutineAcceptFurniture);
            }

            _coroutineAcceptFurniture = StartCoroutine(AcceptFurniture());
        }
    }

    private void WorkEventExit(Collider collider)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void WorkEventGiveStool(Collider collider)
    {
        if (_stackFurniture.IsFull == true)
            return;

        GivStool();
    }

    protected override void CreatFurniture()
    {
        DoubleBedsideTable bedsideTables = Instantiate(_prefabDoubleBedsideTable, _pointSpawner.position, Quaternion.identity);

        _furnitures.Add(bedsideTables);

        OnStartedEffect?.Invoke();
    }

    private IEnumerator AcceptFurniture()
    {
        while (_stackFurniture.GetListStack().Count != 0 && _countFurnitureForCreate != _countFurniture)
        {
            _bedsideTableRelevant = SearchBedsideTable();

            _stackFurniture.RemoveFurniture(_bedsideTableRelevant, gameObject.transform);

            yield return new WaitForSeconds(_delayCoroutine);

            _countFurniture++;

            OnChagedCountFurniture?.Invoke();

            if (_countBoardsForCreate == _countBoard && _countFurnitureForCreate == _countFurniture)
            {
                IsOpen = false;

                if (_coroutineAnimation != null)
                {
                    StopCoroutine(_coroutineAnimation);
                }

                _coroutineAnimation = StartCoroutine(PlayAnimation());
            }
        }
    }

    protected override IEnumerator PlayAnimation()
    {
        _isAnimationPlay = true;

        while (_isAnimationPlay != false)
        {
            _worker.SetBool(_animationWork, _isAnimationPlay);

            yield return new WaitForSeconds(_timeAnimation);

            _isAnimationPlay = false;

            _worker.SetBool(_animationWork, _isAnimationPlay);

            CreatFurniture();
        }
    }

    private BedsideTable SearchBedsideTable()
    {
        foreach (var furniture in _stackFurniture.GetListStack())
        {
            if (furniture is BedsideTable)
            {
                _bedsideTable = (BedsideTable)furniture;
                return _bedsideTable;
            }
        }

        return null;
    }

    private void GivFurniture()
    {
        if (_furnitures.Count == 0) return;

        _furnitures[_furnitures.Count - 1].transform.position = _stackFurniture.GetTransform().position;

        _furnitures[_furnitures.Count - 1].gameObject.transform.SetParent(_stackFurniture.transform);

        _stackFurniture.AddFurnitur(_furnitures[_furnitures.Count - 1]);

        _furnitures.Remove(_furnitures[_furnitures.Count - 1]);

        IsOpen = true;

        _countBoard = 0;
        _countFurniture = 0;

        OnChangedCount?.Invoke();
        OnChagedCountFurniture?.Invoke();
    }
}
