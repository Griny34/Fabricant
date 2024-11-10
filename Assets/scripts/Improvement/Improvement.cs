using System.Collections;
using TMPro;
using UnityEngine;
using Gameplay.Common;
using WalletUser;
using Player;

public abstract class Improvement : MonoBehaviour
{
    private const float _speedBay = 0.001f;

    [SerializeField] private TriggerHandler _triggerHandler;
    [SerializeField] private float _delayStartAction;
    [SerializeField] private int _valueBay;

    [SerializeField] private int _priseOpen;
    [SerializeField] private TextMeshProUGUI _valueOnText;
    [SerializeField] private GameObject _text;
    [SerializeField] private Wallet _wallet;

    private WaitForSeconds _timeCoroutine = new WaitForSeconds(_speedBay);
    private int _valueCounter;
    private Coroutine _coroutine;

    private bool _isOpen =  true;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<JoystickPlayer>(out JoystickPlayer joystickPlayer) == true)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(BayZone());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<JoystickPlayer>(out JoystickPlayer joystickPlayer) == true)
        {
            StopCoroutine(_coroutine);
        }
    }

    public bool GetBoolIsOpen()
    {
        return _isOpen;
    }

    protected void OpenSpawner()
    {
        gameObject.SetActive(false);
        _text.gameObject.SetActive(false);
        Change();
        _triggerHandler.gameObject.SetActive(true);
    }

    protected int GetValueCounter()
    {
        return _valueCounter;
    }

    protected void ChangeValueCounter(int saveValue)
    {
        _valueCounter = saveValue;

        _valueOnText.text = _valueCounter.ToString();
    }

    protected virtual void Change()
    {
    }

    private void SetAsOpen()
    {
        _isOpen = false;
    }

    private IEnumerator BayZone()
    {
        yield return new WaitForSeconds(_delayStartAction);

        while (_wallet.GetMoney() != 0 && _valueCounter <= _priseOpen)
        {
            _wallet.GiveMoney(_valueBay);

            _valueCounter += _valueBay;

            _valueOnText.text = _valueCounter.ToString();

            if (_valueCounter == _priseOpen)
            {
                gameObject.SetActive(false);
                _text.gameObject.SetActive(false);
                SetAsOpen();
                Change();
                _triggerHandler.gameObject.SetActive(true);
            }

            yield return _timeCoroutine;
        }
    }
}