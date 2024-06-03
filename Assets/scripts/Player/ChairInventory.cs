using Gameplay.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairInventory : Inventory
{
    private void Start()
    {
        Upgrade.Instace.OnBuyChairInventory += () =>
        {
            _maxCountItem++;
        };
    }
}
