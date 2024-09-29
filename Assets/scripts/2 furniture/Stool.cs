using UnityEngine;

public class Stool : Furniture
{
    [SerializeField] private int _price;

    protected override int GetPrice()
    {
        return _price;
    }
}
