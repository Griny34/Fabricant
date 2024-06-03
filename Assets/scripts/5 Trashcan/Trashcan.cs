using Gameplay.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    [SerializeField] private TriggerHandler _triggerHandler;
    [SerializeField] private StackMaterial _stackMaterial;
    [SerializeField] private StackFurniture _stackFurniture;

    private Coroutine _coroutine;
    private Material _material;
    private Furniture _furniture;

    private void Start()
    {
        _triggerHandler.OnEnter += col =>
        {
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(ClearStacK());
        };

        _triggerHandler.OnExit += col =>
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        };
    }

    private IEnumerator ClearStacK()
    {
        while (_stackMaterial.GetListMaterial().Count != 0)
        {
            _material = _stackMaterial.GetLastDesk();

            _stackMaterial.RemoveDesk(_material, gameObject.transform);

            yield return new WaitForSeconds(0.5f);
        }

        while(_stackFurniture.GetListStack().Count != 0)
        {
            _furniture = _stackFurniture.GetFurniture();

            _stackFurniture.RemoveFurniture(_furniture, gameObject.transform);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
