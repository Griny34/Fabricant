using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Common;
using UnityEngine;

public abstract class SpawnerFurniture : MonoBehaviour
{
    private const string _animationWork = "Work";

    [SerializeField] protected TriggerHandler _triggerHandler;
    [SerializeField] protected TriggerHandler _ariaSpawner;
    [SerializeField] protected Transform _pointSpawner;
    [SerializeField] protected Furniture _prefabFurniture;

    [SerializeField] protected StackMaterial _stackMaterial;
    [SerializeField] protected StackFurniture _stackFurniture;

    [SerializeField] protected int _countBoardsForCreate;
    [SerializeField] protected int _countFurnitureForCreate;

    [SerializeField] protected Animator _worker;

    private float _timeAnimationMan = 3f;
    protected List<Furniture> _furnitures = new List<Furniture>();
    protected Material _materialeRelevant;
    protected Material _materiale;
    protected Coroutine _coroutine;
    protected int _countBoard;
    protected bool IsOpen = true;
    protected int _countFurniture = 0;
    protected bool _isAnimationPlay = false;
    protected Coroutine _coroutineAnimation;

    public virtual event Action OnStartedEffect;
    public virtual event Action OnChangedCount;
    public virtual event Action OnChagedCountFurniture;

    protected virtual void CreatFurniture()
    {
        Furniture furniture = Instantiate(_prefabFurniture, _pointSpawner.position, Quaternion.identity);

        _furnitures.Add(furniture);

        OnStartedEffect?.Invoke();
    }

    protected virtual IEnumerator PlayAnimation()
    {
        _isAnimationPlay = true;

        while (_isAnimationPlay != false)
        {
            _worker.SetBool(_animationWork, _isAnimationPlay);

            yield return new WaitForSeconds(_timeAnimationMan);

            _isAnimationPlay = false;

            _worker.SetBool(_animationWork, _isAnimationPlay);

            CreatFurniture();
        }
    }

    protected virtual void GivStool()
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

    public virtual int GetCountMaterial()
    {
        return _countBoard;
    }

    public virtual int GetCountFurniture()
    {
        return _countFurniture;
    }
}
