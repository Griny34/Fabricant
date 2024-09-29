using UnityEngine;

public class DoubleBedsideTable : Furniture
{
    [SerializeField] private int _price;

    protected override int GetPrice()
    {
        return _price;
    }
}
