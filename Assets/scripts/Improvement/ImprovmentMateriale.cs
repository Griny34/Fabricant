using UnityEngine;
using UnityEngine.UI;
using Orders;

public class ImprovmentMateriale : Improvement
{
    [SerializeField] private Image _image;
    [SerializeField] private Order _order;
    [SerializeField] private Improvement _improvement;
    [SerializeField] private string _keyPrefs;
    [SerializeField] private string _keyPrefsBool;

    private bool IsOpen => PlayerPrefs.GetInt(_keyPrefsBool) != 0;

    private void Start()
    {
        if (IsOpen)
        {
            OpenSpawner();
            return;
        }

        LoadValueCounter();
    }

    public void SaveValueCounter()
    {
        PlayerPrefs.SetInt(_keyPrefs, GetValueCounter());

        if (GetBoolIsOpen() == false)
        {
            PlayerPrefs.SetInt(_keyPrefsBool, 1);
        }
    }

    protected override void Change()
    {
        _image.gameObject.SetActive(true);

        if (_improvement.GetBoolIsOpen() == false)
        {
            _order.OpenAccess();
        }
    }

    private void LoadValueCounter()
    {
        ChangeValueCounter(PlayerPrefs.GetInt(_keyPrefs));
    }
}
