using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private bool _isOpen;

    public string GetName()
    {
        return _name;
    }

    public bool GetBool()
    {
        return _isOpen;
    }

    public void OpenAccess()
    {
        _isOpen = true;
    }
}
