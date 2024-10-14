using System;
using System.Collections;
using UnityEngine;

public class ChairOnWheelsSpawner : SpawnerFurniture
{
    private const string _animatinWork = "Work";

    [SerializeField] private ChairOnWheels _prefabChairOnWheels;
    //[SerializeField] private int _countDoubleBedsideForCreate;

    private float _delayCoroutine = 0.5f;
    private float _timeAnimation = 3f;
    private Armchair _armchairRelevant;
    private Armchair _armchair;
    private Wheel _wheelRelevant;
    private Wheel _whell;
    private Coroutine _coroutineAcceptFurniture;

    public override event Action OnStartedEffect;
    public override event Action OnChangedCount;
    public override event Action OnChagedCountFurniture;

    private void Start()
    {
        //_triggerHandler.OnEnter += col =>
        //{
        //    if (col.GetComponent<JoystickPlayer>() == null)
        //        return;

        //    if (IsOpen == false)
        //        return;

        //    if (SearcWheel() != null)
        //    {
        //        if (_coroutine != null)
        //        {
        //            StopCoroutine(_coroutine);
        //        }

        //        _coroutine = StartCoroutine(AcceptMaterial());
        //    }

        //    if (SearchArmChair() != null)
        //    {
        //        if (_coroutineAcceptFurniture != null)
        //        {
        //            StopCoroutine(_coroutineAcceptFurniture);
        //        }

        //        _coroutineAcceptFurniture = StartCoroutine(AcceptFurniture());
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

        if (SearcWheel() != null)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(AcceptMaterial());
        }

        if (SearchArmChair() != null)
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

    private void CreatChairOnWhill()
    {
        ChairOnWheels chairOnWheels = Instantiate(_prefabChairOnWheels, _pointSpawner.position, Quaternion.identity);

        _furnitures.Add(chairOnWheels);

        OnStartedEffect?.Invoke();
    }

    private IEnumerator AcceptFurniture()
    {
        while (_stackFurniture.GetListStack().Count != 0 && _countFurnitureForCreate != _countFurniture)
        {
            _armchairRelevant = SearchArmChair();

            _stackFurniture.RemoveFurniture(_armchairRelevant, gameObject.transform);          

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

            yield return new WaitForSeconds(_delayCoroutine);
        }
    }

    private IEnumerator AcceptMaterial()
    {
        while (_stackMaterial.GetListMaterial().Count != 0 && _countBoardsForCreate != _countBoard)
        {
            _wheelRelevant = SearcWheel();

            _stackMaterial.RemoveDesk(_wheelRelevant, gameObject.transform);

            yield return new WaitForSeconds(_delayCoroutine);

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
            _worker.SetBool(_animatinWork, _isAnimationPlay);

            yield return new WaitForSeconds(_timeAnimation);

            _isAnimationPlay = false;

            _worker.SetBool(_animatinWork, _isAnimationPlay);

            CreatChairOnWhill();
        }
    }


    private Wheel SearcWheel()
    {
        foreach (var materiale in _stackMaterial.GetListMaterial())
        {
            if (materiale is Wheel)
            {
                _whell = (Wheel)materiale;
                return _whell;
            }
        }

        return null;
    }

    private Armchair SearchArmChair()
    {
        foreach (var furniture in _stackFurniture.GetListStack())
        {
            if (furniture is Armchair)
            {
                _armchair = (Armchair)furniture;
                return _armchair;
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