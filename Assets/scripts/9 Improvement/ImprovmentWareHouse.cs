using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovmentWareHouse : Improvement
{
    [SerializeField] private string _keyPrefsCount;
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
        PlayerPrefs.SetInt(_keyPrefsCount, GetValueCounter());

        if (GetBoolIsOpen() == false)
        {
            PlayerPrefs.SetInt(_keyPrefsBool, 1);
        }
    }

    private void LoadValueCounter()
    {
        ChangeValueCounter(PlayerPrefs.GetInt(_keyPrefsCount));
    }
}
