using System.Collections;
using UnityEngine;
using Gameplay.Common;
using Currency;

namespace AgavaServices
{
    public class RealizationReward : MonoBehaviour
    {
        private const float _delay = 2f;

        [SerializeField] private RewardService _rewardService;
        [SerializeField] private TriggerHandler _triggerHandler;
        [SerializeField] private SpawnerMoney _spawnerMoney;
        [SerializeField] private float _delaySpawn;
        [SerializeField] private int _priceViewing;

        private WaitForSeconds _delayCorutine = new WaitForSeconds(_delay);
        private Coroutine _coroutine;
        private bool _isOpenReward = true;

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

        public void OpenSpawner()
        {
            _isOpenReward = true;
            _triggerHandler.gameObject.SetActive(true);
        }

        private void WorkEventEnter(Collider collider)
        {
            if (_isOpenReward == true)
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                _coroutine = StartCoroutine(TakeRewardAds());
            }
        }

        private void WorkEventExit(Collider collider)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private IEnumerator TakeRewardAds()
        {
            yield return _delayCorutine;

            _isOpenReward = false;

            _rewardService.ShowRewardAds();
        }
    }
}