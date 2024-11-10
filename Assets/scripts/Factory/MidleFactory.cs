using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Common;
using Player;

namespace Factory
{
    public class MidleFactory : MonoBehaviour
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
        private Furniture _relevantFurniture;
        private Furniture _requiredFurniture;
        private int _countFurniture = 0;
        private bool IsOpen = true;
        private Coroutine _coroutineAnimation;
        private Coroutine _coroutine;

        public event Action OnStartedEffect;
        public event Action OnChagedCountFurniture;

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

        public int GetCountFurniture()
        {
            return _countFurniture;
        }

        private void WorkEventEnter(Collider collider)
        {
            if (collider.GetComponent<JoystickPlayer>() == null)
                return;

            if (IsOpen == false)
                return;

            if (SearchFurniture() != null)
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                _coroutine = StartCoroutine(AcceptFurniture());
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
        private IEnumerator AcceptFurniture()
        {
            while (_stackFurniture.GetListStack().Count != 0 && _spawnerFurniture.GetRecipe().CountFurniture != _countFurniture)
            {
                _relevantFurniture = SearchFurniture();

                _stackFurniture.RemoveFurniture(_relevantFurniture, gameObject.transform);

                yield return _delay;

                _countFurniture++;

                OnChagedCountFurniture?.Invoke();

                if (_spawnerFurniture.GetRecipe().CountFurniture == _countFurniture)
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

        private Furniture SearchFurniture()
        {
            foreach (var furniture in _stackFurniture.GetListStack())
            {
                if (furniture.GetType() == _spawnerFurniture.GetRecipe().FurnitureForImprovement.GetType())
                {
                    _requiredFurniture = furniture;
                    return _requiredFurniture;
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

            _countFurniture = 0;

            OnChagedCountFurniture?.Invoke();
        }

        private void CreateFurniture()
        {
            _furnitures.Add(_spawnerFurniture.CreateFurniture());

            OnStartedEffect?.Invoke();
        }
    }
}