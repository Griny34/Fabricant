using Gameplay.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskInventory : Inventory
{
    private void Start()
    {
        Upgrade.Instace.OnBuyDeskInventory += () =>
        {
            _maxCountItem++;
        };
    }
}
