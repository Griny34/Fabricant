using Gameplay.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StoolSpawner : SpawnerFurniture
{
    [SerializeField] private Stool _stool;

    private Board _boardRelevant;
    private Board _board;

    public override event Action OnStartEffect;
    public override event Action OnChangeCount;

    private void Start()
    {
        _triggerHandler.OnEnter += col =>
        {
            if (col.GetComponent<JoystickPlayer>() == null) return;

            if (IsOpen == false) return;

            if(GetCountFurniture() != 0) return;

            if(_stackMaterial.GetListMaterial().Count == 0) return;

            if(SearchMateriale() != null)
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                _coroutine = StartCoroutine(AcceptMaterial());
            }         
        };

        _triggerHandler.OnExit += col =>
        {
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }          
        };


        _ariaSpawner.OnEnter += col =>
        {
            if (_stackFurniture.IsFull == true) return;

            GivStool();
        };
    }

    protected override void CreatFurniture()
    {
        Stool stool = Instantiate(_stool, _pointSpawner.position, Quaternion.identity);

        _furnitures.Add(stool);

        OnStartEffect?.Invoke();       
    }

    protected IEnumerator AcceptMaterial()
    {
        while (_stackMaterial.GetListMaterial().Count != 0 && _countBoardsForCreate != _countBoard)
        {
            _boardRelevant = SearchMateriale();

            _stackMaterial.RemoveDesk(_boardRelevant, gameObject.transform);

            _countBoard++;

            OnChangeCount?.Invoke();

            if (_countBoardsForCreate == _countBoard)
            {
                IsOpen = false;

                if (_coroutineAnimation != null)
                {
                    StopCoroutine(_coroutineAnimation);
                }

                _coroutineAnimation = StartCoroutine(PlayAnimation());
            }

            yield return new WaitForSeconds(0.5f);
        }       
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

        OnChangeCount?.Invoke();
    }

    public override int GetCountMatiriale()
    {
        return _countBoard;
    }
}
