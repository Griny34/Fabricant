using UnityEngine;

public class Money : MonoBehaviour
{
    private const string _keyPrefsMoney = "money";

    [SerializeField] private int _value;
    [SerializeField] private int _upgradeMoney;

    private int _startMoney = 10;

    public static Money Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(_keyPrefsMoney))
        {
            _value = PlayerPrefs.GetInt(_keyPrefsMoney);
        }
        else
        {
            _value = _startMoney;
        }
    }

    private void OnEnable()
    {
        Upgrade.Instace.OnBuyMoney += UpgradeMoney;
    }

    private void OnDisable()
    {
        Upgrade.Instace.OnBuyMoney -= UpgradeMoney;
    }

    public int GetMoneyValue()
    {
        return _value;
    }

    private void UpgradeMoney()
    {
        _value += _upgradeMoney;

        PlayerPrefs.SetInt(_keyPrefsMoney, _value);
    }
}
