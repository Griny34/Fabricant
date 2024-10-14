using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Common;

namespace Factory
{
    public class EasyFactory : MonoBehaviour
    {
        private const string _animationWork = "Work";
        private const float _delyeCoroutine = 0.5f;
        private const float _timeAnimation = 3f;

        [SerializeField] private SpawnerFurniture _spawnerFurniture;
        [SerializeField] private StackMaterial _stackMaterial;
        [SerializeField] private StackFurniture _stackFurniture;

        [SerializeField] private TriggerHandler _triggerHandler;
        [SerializeField] private TriggerHandler _ariaSpawner;

        [SerializeField] private Animator _worker;

        private List<Furniture> _furnitures = new List<Furniture>();
        private WaitForSeconds _delay = new WaitForSeconds(_delyeCoroutine);
        private WaitForSeconds _timeWork = new WaitForSeconds(_timeAnimation);
        private bool _isAnimationPlay;
        private Material _relevantMaterial;
        private Material _requiredMaterial;
        private int _countMateriale = 0;
        private bool IsOpen = true;
        private Coroutine _coroutineAnimation;
        private Coroutine _coroutine;

        public event Action OnStartedEffect;
        public event Action OnChangedCount;

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
        }

        public int GetCountMateriale()
        {
            return _countMateriale;
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

        private IEnumerator PlayAnimation()
        {
            _isAnimationPlay = true;

            while (_isAnimationPlay != false)
            {
                _worker.SetBool(_animationWork, _isAnimationPlay);

                yield return _timeWork;

                _isAnimationPlay = false;

                _worker.SetBool(_animationWork, _isAnimationPlay);
            }

            CreateFurniture();
        }
        private IEnumerator AcceptMaterial()
        {
            while (_stackMaterial.GetListMaterial().Count != 0 && _spawnerFurniture.GetRecipe().CountMateriale != _countMateriale)
            {
                _relevantMaterial = SearchMateriale();

                _stackMaterial.RemoveDesk(_relevantMaterial, _spawnerFurniture.gameObject.transform);

                _countMateriale++;

                OnChangedCount?.Invoke();

                if (_spawnerFurniture.GetRecipe().CountMateriale == _countMateriale)
                {
                    IsOpen = false;

                    if (_coroutineAnimation != null)
                    {
                        StopCoroutine(_coroutineAnimation);
                    }

                    _coroutineAnimation = StartCoroutine(PlayAnimation());
                }

                yield return _delay;
            }
        }

        private Material SearchMateriale()
        {
            foreach (var materiale in _stackMaterial.GetListMaterial())
            {
                if (materiale.GetType() == _spawnerFurniture.GetRecipe().MaterialForImprovement.GetType())
                {
                    _requiredMaterial = materiale;
                    return _requiredMaterial;
                }
            }

            return null;
        }

        private void GivStool()
        {
            if (_furnitures.Count == 0) return;

            _furnitures[_furnitures.Count - 1].transform.position = _stackFurniture.GetTransform().position;

            _furnitures[_furnitures.Count - 1].gameObject.transform.SetParent(_stackFurniture.transform);

            _stackFurniture.AddFurnitur(_furnitures[_furnitures.Count - 1]);

            _furnitures.Remove(_furnitures[_furnitures.Count - 1]);

            IsOpen = true;

            _countMateriale = 0;

            OnChangedCount?.Invoke();
        }

        private void CreateFurniture()
        {
            _furnitures.Add(_spawnerFurniture.CreateFurniture());

            OnStartedEffect?.Invoke();
        }
    }
}