using Gameplay.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerFurniture : MonoBehaviour
{
    [SerializeField] protected TriggerHandler _triggerHandler;
    [SerializeField] protected TriggerHandler _ariaSpawner;
    [SerializeField] protected Transform _pointSpawner;
    [SerializeField] protected Furniture _prefabFurniture;

    [SerializeField] protected StackMaterial _stackMaterial;
    [SerializeField] protected StackFurniture _stackFurniture;

    [SerializeField] protected int _countBoardsForCreate;
    [SerializeField] protected int _countFurnitureForCreate;

    [SerializeField] protected Animator _worker;

    protected List<Furniture> _furnitures = new List<Furniture>();
    protected Material _materialeRelevant;
    protected Material _materiale;
    protected Coroutine _coroutine;
    protected int _countBoard;
    protected bool IsOpen = true;
    protected int _countFurniture = 0;
    protected bool _isAnimationPlay = false;
    protected Coroutine _coroutineAnimation;

    public virtual event Action OnStartEffect;
    public virtual event Action OnChangeCount;
    public virtual event Action OnChageCountFurniture;

    private void Start()
    {
        //_triggerHandler.OnEnter += col =>
        //{
        //    if (col.GetComponent<JoystickPlayer>() == null) return;

        //    if (IsOpen == false) return;

        //    if (GetCountFurniture() != 0) return;

        //    if (_stackMaterial.GetListMaterial().Count == 0) return;

        //    if (_coroutine != null)
        //    {
        //        StopCoroutine(_coroutine);
        //    }

        //    _coroutine = StartCoroutine(AcceptMaterial());
        //};

        _ariaSpawner.OnEnter += col =>
        {
            if (_stackFurniture.IsFull == true) return;

            GivStool();
        };
    }

    protected virtual void CreatFurniture()
    {
        Furniture furniture = Instantiate(_prefabFurniture, _pointSpawner.position, Quaternion.identity);

        _furnitures.Add(furniture);

        OnStartEffect?.Invoke();
    }

    //protected virtual IEnumerator AcceptMaterial()
    //{
    //    while (_stackMaterial.GetListMaterial().Count != 0 && _countBoardsForCreate != _countBoard)
    //    {
    //        _materialeRelevant = SearchMateriale();

    //        _stackMaterial.RemoveDesk(_materialeRelevant, gameObject.transform);

    //        yield return new WaitForSeconds(0.5f);

    //        _countBoard++;

    //        OnChangeCount?.Invoke();

    //        if (_countBoardsForCreate == _countBoard)
    //        {
    //            IsOpen = false;

    //            if (_coroutineAnimation != null)
    //            {
    //                StopCoroutine(_coroutineAnimation);
    //            }

    //            _coroutineAnimation = StartCoroutine(PlayAnimation());
    //        }
    //    }
    //}

    protected virtual IEnumerator PlayAnimation()
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

    //protected virtual Material SearchMateriale()
    //{
    //    foreach (var material in _stackMaterial.GetListMaterial())
    //    {
    //        if (material is Board)
    //        {
    //            _materiale = (Board)material;
    //            return _materiale;
    //        }
    //    }

    //    return null;
    //}

    protected virtual void GivStool()
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

    public virtual int GetCountMatiriale()
    {
        return _countBoard;
    }

    public virtual int GetCountFurniture()
    {
        return _countFurniture;
    }
}
