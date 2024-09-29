using System.Collections;
using Gameplay.Common;
using UnityEngine;

public class RealizationReward : MonoBehaviour
{
    [SerializeField] private RewardService _rewardService;
    [SerializeField] private TriggerHandler _triggerHandler;
    [SerializeField] private SpawnerMoney _spawnerMoney;
    [SerializeField] private float _delay;
    [SerializeField] private float _delaySpawn;
    [SerializeField] private int _priceViewing;

    private Coroutine _coroutine;
    private bool _isOpenReward = true;
    //private bool _isWorkCoroutine = true;

    private void Start()
    {       
        //_triggerHandler.OnEnter += col =>
        //{
        //    if (_isOpenReward == true)
        //    {
        //        if (_coroutine != null)
        //        {
        //            StopCoroutine(_coroutine);
        //        }

        //        _coroutine = StartCoroutine(TakeRewardAds());
        //    }
        //};

        //_triggerHandler.OnExit += col =>
        //{
        //    StopCoroutine(_coroutine);
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

    public void OpenSpawner()
    {
        //_isWorkCoroutine = true;
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
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator TakeRewardAds()
    {
        yield return new WaitForSeconds(_delay);      

        _isOpenReward = false;

        _rewardService.ShowRewardAds();

        //_isWorkCoroutine = false;
    }
}
