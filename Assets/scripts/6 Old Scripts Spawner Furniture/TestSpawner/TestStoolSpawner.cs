using System;
using System.Collections;
using UnityEngine;

public class TestStoolSpawner : TestSpawnerFurniture
{
    private const string _animationWork = "Work";

    [SerializeField] private Stool _stool;

    private float _delayCoroutine = 0.5f;
    private float _timeAnimation = 3f;
    private Board _boardRelevant;
    private Board _board;
    public override event Action OnChangedCount;
    public override event Action OnStartedEffect;

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
        Stool stool = Instantiate(_stool, _pointSpawner.position, Quaternion.identity);

        _furnitures.Add(stool);

        OnStartedEffect?.Invoke();
    }

    protected IEnumerator AcceptMaterial()
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

    //protected override void GivStool()
    //{
    //    if (_furnitures.Count == 0) return;

    //    _furnitures[_furnitures.Count - 1].transform.position = _stackFurniture.GetTransform().position;

    //    _furnitures[_furnitures.Count - 1].gameObject.transform.SetParent(_stackFurniture.transform);

    //    _stackFurniture.AddFurnitur(_furnitures[_furnitures.Count - 1]);

    //    _furnitures.Remove(_furnitures[_furnitures.Count - 1]);

    //    IsOpen = true;

    //    _countBoard = 0;

    //    OnChangedCount?.Invoke();
    //}

    public override int GetCountMaterial()
    {
        return _countBoard;
    }

    public void CreateFurniture()
    {
        throw new NotImplementedException();
    }
}
