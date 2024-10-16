using System;
using System.Collections;
using UnityEngine;

public class BedsideSpawner : SpawnerFurniture
{
    [SerializeField] private BedsideTable _bedsideTable;

    private float _delayCoroutine = 0.5f;
    private Board _boardRelevant;
    private Board _board;

    public override event Action OnStartedEffect;
    public override event Action OnChangedCount;

    private void Start()
    {
        //_triggerHandler.OnEnter += col =>
        //{
        //    if (col.GetComponent<JoystickPlayer>() == null)
        //        return;

        //    if (IsOpen == false)
        //        return;

        //    if (GetCountFurniture() != 0)
        //        return;

        //    if (_stackMaterial.GetListMaterial().Count == 0)
        //        return;

        //    if (SearchMateriale() != null)
        //    {
        //        if (_coroutine != null)
        //        {
        //            StopCoroutine(_coroutine);
        //        }

        //        _coroutine = StartCoroutine(AcceptMaterial());
        //    }
        //};

        //_triggerHandler.OnExit += col =>
        //{
        //    if (_coroutine != null)
        //    {
        //        StopCoroutine(_coroutine);
        //    }
        //};

        //_ariaSpawner.OnEnter += col =>
        //{
        //    if (_stackFurniture.IsFull == true) return;

        //    GivStool();
        //};
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

        if (GetCountFurniture() != 0)
            return;

        if (_stackMaterial.GetListMaterial().Count == 0)
            return;

        if (SearchMateriale() != null)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(AcceptMaterial());
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
        BedsideTable bedsideTable = Instantiate(_bedsideTable, _pointSpawner.position, Quaternion.identity);

        _furnitures.Add(bedsideTable);

        OnStartedEffect?.Invoke();
    }

    protected  IEnumerator AcceptMaterial()
    {
        while (_stackMaterial.GetListMaterial().Count != 0 && _countBoardsForCreate != _countBoard)
        {
            _boardRelevant = SearchMateriale();

            _stackMaterial.RemoveDesk(_boardRelevant, gameObject.transform);

            _countBoard++;

            OnChangedCount?.Invoke();

            if (_countBoardsForCreate == _countBoard)
            {
                IsOpen = false;

                if (_coroutineAnimation != null)
                {
                    StopCoroutine(_coroutineAnimation);
                }

                _coroutineAnimation = StartCoroutine(PlayAnimation());
            }

            yield return new WaitForSeconds(_delayCoroutine);
        }
    }

    protected Board SearchMateriale()
    {
        foreach (var materiale in _stackMaterial.GetListMaterial())
        {
            if (materiale is Board)
            {
                _board = (Board)materiale;
                return _board;
            }
        }

        return null;
    }

    protected override void GivStool()
    {
        if (_furnitures.Count == 0) return;

        _furnitures[_furnitures.Count - 1].transform.position = _stackFurniture.GetTransform().position;

        _furnitures[_furnitures.Count - 1].gameObject.transform.SetParent(_stackFurniture.transform);

        _stackFurniture.AddFurnitur(_furnitures[_furnitures.Count - 1]);

        _furnitures.Remove(_furnitures[_furnitures.Count - 1]);

        IsOpen = true;

        _countBoard = 0;

        OnChangedCount?.Invoke();
    }

    public override int GetCountMaterial()
    {
        return _countBoard;
    }
}