using System;
using System.Collections;
using UnityEngine;

public class ArmchairSpawner : SpawnerFurniture
{
    private const string _animationWork = "Work";

    [SerializeField] private Armchair _prefabArmchair;

    private float _delayCoroutine = 0.5f;
    private float _timeAnimation = 3f;
    private Chair _chairRelevant;
    private Chair _chair;
    private Leather _leatherRelevant;
    private Leather _leather;
    private Coroutine _coroutineAcceptFurniture;

    public override event Action OnStartedEffect;
    public override event Action OnChangedCount;
    public override event Action OnChagedCountFurniture;

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

        if (SearchMateriale() != null)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(AcceptMaterial());
        }

        if (SearchChair() != null)
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
        Armchair armchair = Instantiate(_prefabArmchair, _pointSpawner.position, Quaternion.identity);

        _furnitures.Add(armchair);

        OnStartedEffect?.Invoke();
    }

    private IEnumerator AcceptFurniture()
    {
        while (_stackFurniture.GetListStack().Count != 0 && _countFurnitureForCreate != _countFurniture)
        {
            _chairRelevant = SearchChair();

            _stackFurniture.RemoveFurniture(_chairRelevant, gameObject.transform);

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

    protected IEnumerator AcceptMaterial()
    {
        while (_stackMaterial.GetListMaterial().Count != 0 && _countBoardsForCreate != _countBoard)
        {
            _leatherRelevant = SearchMateriale();

            _stackMaterial.RemoveDesk(_leatherRelevant, gameObject.transform);

            _countBoard++;

            OnChangedCount?.Invoke();

            if (_countBoardsForCreate == _countBoard && _countFurnitureForCreate == _countFurniture)
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

        if (_countBoardsForCreate == _countBoard)
        {
            if (_coroutineAcceptFurniture != null)
            {
                StopCoroutine(_coroutineAcceptFurniture);
            }

            _coroutineAcceptFurniture = StartCoroutine(AcceptFurniture());
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

    private Leather SearchMateriale()
    {
        foreach (var materiale in _stackMaterial.GetListMaterial())
        {
            if (materiale is Leather)
            {
                _leather = (Leather)materiale;
                return _leather;
            }
        }

        return null;
    }

    private Chair SearchChair()
    {
        foreach (var furniture in _stackFurniture.GetListStack())
        {
            if (furniture is Chair)
            {
                _chair = (Chair)furniture;
                return _chair;
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
        _countFurniture = 0;

        OnChangedCount?.Invoke();
        OnChagedCountFurniture?.Invoke();
    }
}
