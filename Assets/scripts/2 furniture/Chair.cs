using UnityEngine;

public class Chair : Furniture
{
    [SerializeField] private int _price;

    protected override int GetPrice()
    {
        return _price;
    }
}
