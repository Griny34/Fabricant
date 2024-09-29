using UnityEngine.UI;
using UnityEngine;

public class ImprovmentSpawner : Improvement
{
    [SerializeField] private Order _order;
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
        _order.OpenAccess();

        _imageSpawnFurnitur.gameObject.SetActive(false);
        _tarif.gameObject.SetActive(true);
    }

    private void LoadValueCounter()
    {
        ChangeValueCounter(PlayerPrefs.GetInt(_keyPrefsCount));
    }
}