using Gameplay.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TibleSpawner : SpawnerFurniture
{
    [SerializeField] private Table _prefabTable;
    [SerializeField] private int _countDoubleBedsideForCreate;

    private DoubleBedsideTable _doubleBedsideTableRelevant;
    private DoubleBedsideTable _doubleBedsideTable;
    private Coroutine _coroutineAcceptFurniture;

    public override event Action OnStartEffect;
    public override event Action OnChangeCount;
    public override event Action OnChageCountFurniture;

    private void Start()
    {
        _triggerHandler.OnEnter += col =>
        {
            if (col.GetComponent<JoystickPlayer>() == null) return;

            if (IsOpen == false) return;

            if (SearchDoubleBedside() != null)
            {
                if (_coroutineAcceptFurniture != null)
                {
                    StopCoroutine(_coroutineAcceptFurniture);
                }

                _coroutineAcceptFurniture = StartCoroutine(AcceptFurniture());
            }
        };

        _ariaSpawner.OnEnter += col =>
        {
            if (_stackFurniture.IsFull == true) return;

            GivFurniture();
        };
    }

    protected override void CreatFurniture()
    {
        Table table = Instantiate(_prefabTable, _pointSpawner.position, Quaternion.identity);

        _furnitures.Add(table);

        OnStartEffect?.Invoke();
    }

    private IEnumerator AcceptFurniture()
    {
        while (_stackFurniture.GetListStack().Count != 0 && _countFurnitureForCreate != _countFurniture)
        {
            _doubleBedsideTableRelevant = SearchDoubleBedside();

            _stackFurniture.RemoveFurniture(_doubleBedsideTableRelevant, gameObject.transform);

            yield return new WaitForSeconds(0.5f);

            _countFurniture++;

            OnChageCountFurniture?.Invoke();

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

    private DoubleBedsideTable SearchDoubleBedside()
    {
        foreach (var furniture in _stackFurniture.GetListStack())
        {
            if (furniture is DoubleBedsideTable)
            {
                _doubleBedsideTable = (DoubleBedsideTable)furniture;
                return _doubleBedsideTable;
            }
        }

        return null;
    }

    protected override IEnumerator PlayAnimation()
    {
        _isAnimationPlay = true;

        while (_isAnimationPlay != false)
        {
            _worker.SetBool("Work", _isAnimationPlay);

            yield return new WaitForSeconds(3f);

            _isAnimationPlay = false;

            _worker.SetBool("Work", _isAnimationPlay);

            CreatFurniture();
        }
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

        OnChangeCount?.Invoke();
        OnChageCountFurniture?.Invoke();
    }
}
