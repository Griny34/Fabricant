using System.Collections;
using Gameplay.Common;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    private const float _delyeCoroutine = 0.5f;

    [SerializeField] private TriggerHandler _triggerHandler;
    [SerializeField] private StackMaterial _stackMaterial;
    [SerializeField] private StackFurniture _stackFurniture;

    private WaitForSeconds _timeCoroutine = new WaitForSeconds(_delyeCoroutine);
    private Coroutine _coroutine;
    private Material _material;
    private Furniture _furniture;

    private void Start()
    {
        //_triggerHandler.OnEnter += col =>
        //{
        //    if (_coroutine != null)
        //    {
        //        StopCoroutine(_coroutine);
        //    }

        //    _coroutine = StartCoroutine(ClearStacK());
        //};

        //_triggerHandler.OnExit += col =>
        //{
        //    if (_coroutine != null)
        //    {
        //        StopCoroutine(_coroutine);
        //    }
        //};
    }

    private void OnEnable()
    {
        _triggerHandler.OnEnter += WorkEventEnter;
        _triggerHandler.OnExit += WorkEventExit;
    }

    private void OnDisable()
    {
        _triggerHandler.OnEnter -= WorkEventEnter;
        _triggerHandler.OnExit -= WorkEventExit;
    }

    private void WorkEventEnter(Collider collider)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ClearStacK());
    }

    private void WorkEventExit(Collider collider)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator ClearStacK()
    {
        while (_stackMaterial.GetListMaterial().Count != 0)
        {
            _material = _stackMaterial.GetLastDesk();

            _stackMaterial.RemoveDesk(_material, gameObject.transform);

            yield return _timeCoroutine;
        }

        while (_stackFurniture.GetListStack().Count != 0)
        {
            _furniture = _stackFurniture.GetFurniture();

            _stackFurniture.RemoveFurniture(_furniture, gameObject.transform);

            yield return _timeCoroutine;
        }
    }
}