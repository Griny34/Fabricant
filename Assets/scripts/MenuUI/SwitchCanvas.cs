using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCanvas : SwitchCanvasMenu
{
    [SerializeField] private GameObject _portraitOrders;
    [SerializeField] private GameObject _landscapeOrders;

    protected override void SwitchVerticalView()
    {
        _portraitOrders.SetActive(true);

        _landscapeOrders.SetActive(false);
    }

    protected override void SwitchHorizontalView()
    {
        _portraitOrders.SetActive(false);

        _landscapeOrders.SetActive(true);
    }
}
