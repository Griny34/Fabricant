using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private bool _isOpen;

    public string GetName()
    {
        return _name;
    }

    public bool GetIsOpen()
    {
        return _isOpen;
    }

    public void OpenAccess()
    {
        _isOpen = true;
    }
}
