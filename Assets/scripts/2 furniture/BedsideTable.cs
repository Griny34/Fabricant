using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedsideTable : Furniture
{
    [SerializeField] private int _price;

    protected override int GetPrice()
    {
        return _price;
    }
}
