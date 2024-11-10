using UnityEngine.UI;
using UnityEngine;
using Orders;

public class ImprovmentChairOnWheel : Improvement
{
    [SerializeField] private Order _order;
    [SerializeField] private ImprovmentMateriale _improvement;
    [SerializeField] private string _keyPrefsCount;
    [SerializeField] private string _keyPrefsBool;

    [SerializeField] private Image _imageSpawnFurnitur;
    [SerializeField] private GameObject _tarif;

    private bool IsOpen => PlayerPrefs.GetInt(_keyPrefsBool) != 0;

    private void Start()
    {
        if (IsOpen)
        {
            OpenSpawner();
            _imageSpawnFurnitur.gameObject.SetActive(false);
            _tarif.gameObject.SetActive(true);
            return;
        }

        LoadValueCounter();
    }

    public void SaveValueCounter()
    {
        PlayerPrefs.SetInt(_keyPrefsCount, GetValueCounter());

        if (GetBoolIsOpen() == false)
        {
            PlayerPrefs.SetInt(_keyPrefsBool, 1);
        }
    }

    protected override void Change()
    {
        _imageSpawnFurnitur.gameObject.SetActive(false);
        _tarif.gameObject.SetActive(true);

        if (_improvement.GetBoolIsOpen() == false)
        {
            _order.OpenAccess();
        }
    }

    private void LoadValueCounter()
    {
        ChangeValueCounter(PlayerPrefs.GetInt(_keyPrefsCount));
    }
}
