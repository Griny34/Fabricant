using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewRejection : MonoBehaviour
{
    [SerializeField] private Upgrade _upgrade;
    [SerializeField] private Animator _animator;

    private string _click = "click";
    private bool _isPlay = false;
    private Coroutine _coroutine; 

    private void Start()
    {
        _upgrade.OnCanNotBuySpeed += () =>
        {
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(PlayAnimation());

            //_animator.SetTrigger(_click);
        };

        _upgrade.OnCanNotBayDesk += () =>
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(PlayAnimation());
            //_animator.SetTrigger(_click);
        };

        _upgrade.OnCanNotBayChair += () =>
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(PlayAnimation());
            //_animator.SetTrigger(_click);
        };

        _upgrade.OnCanNotBuyMoney += () =>
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(PlayAnimation());
            //_animator.SetTrigger(_click);
        };
    }

    private IEnumerator PlayAnimation()
    {
        _isPlay = true;

        while (_isPlay == true)
        {
            _animator.SetBool(_click, true);

            yield return new WaitForSecondsRealtime(1f);

            _animator.SetBool(_click, false);
            _isPlay = false;
        }
    }
}
