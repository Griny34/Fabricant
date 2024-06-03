using Gameplay.Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImprovmentMateriale : Improvement
{
    [SerializeField] private Image _image;
    [SerializeField] private Order _order;
    [SerializeField] private Improvement _improvement;
    [SerializeField] private string _keyPrefs;
    [SerializeField] private string _keyPrefsBool;

    private bool _isOpen => PlayerPrefs.GetInt(_keyPrefsBool) != 0;

    private void Start()
    {
        if (_isOpen)
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
