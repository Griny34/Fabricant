using System.Collections;
using UnityEngine;

namespace UpgradeSkills
{
    public class ViewRejection : MonoBehaviour
    {
        private const string _click = "click";
        private const float _timeCoritine = 1f;

        [SerializeField] private Upgrade _upgrade;
        [SerializeField] private Animator _animator;

        private WaitForSeconds _wait = new WaitForSeconds(_timeCoritine);
        private bool _isPlay = false;
        private Coroutine _coroutine;

        private void OnEnable()
        {
            _upgrade.OnCanNotBuySpeed += StartCorutineAnimation;
            _upgrade.OnCanNotBayDesk += StartCorutineAnimation;
            _upgrade.OnCanNotBuyMoney += StartCorutineAnimation;
        }

        private void OnDisable()
        {
            _upgrade.OnCanNotBuySpeed += StartCorutineAnimation;
            _upgrade.OnCanNotBayDesk += StartCorutineAnimation;
            _upgrade.OnCanNotBuyMoney += StartCorutineAnimation;
        }

        private void StartCorutineAnimation()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(PlayAnimation());
        }

        private IEnumerator PlayAnimation()
        {
            _isPlay = true;

            while (_isPlay == true)
            {
                _animator.SetBool(_click, true);

                yield return _wait;

                _animator.SetBool(_click, false);
                _isPlay = false;
            }
        }
    }
}