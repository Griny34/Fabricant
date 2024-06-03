using Agava.YandexGames;
using DG.Tweening;
using Gameplay.Common;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WareHause : MonoBehaviour
{
    [SerializeField] private ListPrefabFurniture _listPrefabFurniture;
    [SerializeField] private string _keySave;
    [SerializeField] private TriggerHandler _triggerHandler;
    [SerializeField] private StackFurniture _stackFurniture;

    [SerializeField] private float _delayStartAction;
    [SerializeField] private Transform _point;
    private float _delay = 0.5f;
    private bool _isOpen = true;
    private Furniture _furniture;
    private Coroutine _coroutineGive;
    private Coroutine _coroutineTake;

    private List<Furniture> _furnitures = new List<Furniture>();

    private void Awake()
    {
        LoadFurniture();
    }

    private void Start()
    {
        _triggerHandler.OnEnter += col =>
        {
            if(_stackFurniture.GetListStack().Count != 0 && _isOpen == true)
            {
                if(_coroutineGive != null)
                {
                    StopCoroutine(_coroutineGive);
                }

                _coroutineGive = StartCoroutine(GiveFuniture());
            }

            if(_stackFurniture.GetListStack().Count == 0 && _isOpen == false)
            {
                if (_coroutineTake != null)
                {
                    StopCoroutine(_coroutineTake);
                }

                _coroutineTake = StartCoroutine(TakeFurniture());
            }
        };
    }

    private IEnumerator GiveFuniture()
    {
        yield return new WaitForSeconds(_delayStartAction);

        while (_stackFurniture != null && _isOpen != false)
        {
            _furniture = _stackFurniture.GetFurniture();

            GiveFuniture(_furniture, _point);

            _stackFurniture.RemoveFurnitur(_furniture);

            yield return new WaitForSeconds(_delay);

            _furnitures.Add(_furniture);

            PlayerPrefs.SetString(_keySave, _furnitures[_furnitures.Count - 1].GetName());

            _isOpen = false;

            yield return null;
        }
    }

    private void GiveFuniture(Furniture furniture, Transform pointDestroy)
    {
        if (furniture == null) return;

        furniture.transform.SetParent(null);

        furniture.transform.DOJump(pointDestroy.position, 1, 1, _delay).OnComplete(
            () =>
            {
                _stackFurniture.RemoveFurnitur(furniture);              
            });
    }

    private IEnumerator TakeFurniture()
    {
        yield return new WaitForSeconds(_delayStartAction);

        while (_stackFurniture != null && _isOpen != true)
        {
            TakeFurnitur();
            _isOpen = true;

            PlayerPrefs.DeleteKey(_keySave);

            yield return null;
        }
    }

    private void TakeFurnitur()
    {
        if (_furnitures.Count == 0) return;

        _furnitures[_furnitures.Count - 1].transform.position = _stackFurniture.GetTransform().position;

        _furnitures[_furnitures.Count - 1].gameObject.transform.SetParent(_stackFurniture.transform);

        _stackFurniture.AddFurnitur(_furnitures[_furnitures.Count - 1]);

        _furnitures.Remove(_furnitures[_furnitures.Count - 1]);
    }

    private void LoadFurniture()
    {
        if (PlayerPrefs.HasKey(_keySave))
        {
            foreach(Furniture furniture in _listPrefabFurniture.GetListPrefabFurniture())
            {
                if(furniture.GetName() == PlayerPrefs.GetString(_keySave))
                {
                    _furniture = Instantiate(furniture, _point.position, Quaternion.identity);

                    _furnitures.Add(_furniture);
                    _isOpen = false;
                }
            }
        }
    }
}
