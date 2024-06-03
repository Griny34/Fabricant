using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBedsideTable : Furniture
{
    [SerializeField] private int _price;

    protected override int GivePrice()
    {
        return _price;
    }
}
