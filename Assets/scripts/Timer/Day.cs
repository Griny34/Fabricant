using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Image _image;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;
    private float _startValue;
    private float _currentValue;

    private void Awake()
    {
        _image.fillAmount = 1;       
    }

    private void Start()
    {
        _startValue = MatchModel.Instace.CurrentMatch.Time;

        _timer.OnTick += seconds =>
        {
            _currentValue = _timer.GetTimeLife();

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(ChangeDay());
        };
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
