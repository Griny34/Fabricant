using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameTime
{
    public class Day : MonoBehaviour
    {
        [SerializeField] private Timer _timer;
        [SerializeField] private Image _image;
        [SerializeField] private float _speed;
        [SerializeField] private MatchModel _matchModel;

        private Coroutine _coroutine;
        private float _startValue;
        private float _currentValue;

        private void Awake()
        {
            _image.fillAmount = 1;
        }

        private void Start()
        {
            _startValue = _matchModel.CurrentMatch.Time;
        }

        private void OnEnable()
        {
            _timer.OnTick += WalkDay;
        }

        private void OnDisable()
        {
            _timer.OnTick -= WalkDay;
        }

        private void WalkDay(int seconds)
        {
            _currentValue = _timer.GetTimeLife();

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(ChangeDay());
        }

        private IEnumerator ChangeDay()
        {
            while (true)
            {
                _image.fillAmount = Mathf.MoveTowards(_image.fillAmount, _currentValue / _startValue, _speed);

                yield return null;
            }
        }
    }
}